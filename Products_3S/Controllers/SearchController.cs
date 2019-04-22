using log4net;
using Products_3S.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Products_3S.Controllers
{
    public class SearchController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductsController));
        private ProductDBContext db = new ProductDBContext();

        public ActionResult DisplaySearch()
        {
            ProductUnitModelView model = new ProductUnitModelView()
            {
                Units = db.Units.Select(i => new SelectListItem { Value = i.ID.ToString(), Text = i.UnitName }),
                Suppliers = db.Suppliers.Select(i => new SelectListItem { Value = i.SupplierID.ToString(), Text = i.SupplierName })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplaySearch(Product product)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SelectCategory")
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[Products_search]"
                };
                SqlParameter parameter;

                parameter = sqlCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar);
                parameter.Size = 50;
                parameter.Value = product.ProductName;

                parameter = sqlCommand.Parameters.Add("@UnitId", SqlDbType.Int);
                parameter.Value = product.QuantityPerUnit;

                parameter = sqlCommand.Parameters.Add("@ReorderLevel", SqlDbType.Int);
                parameter.Value = product.ReorderLevel;

                parameter = sqlCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                parameter.Value = product.UnitPrice;

                parameter = sqlCommand.Parameters.Add("@UnitOnOrder", SqlDbType.Int);
                parameter.Value = product.UnitsOnOrder;


                parameter = sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int);
                parameter.Value = product.SupplierID;

                DbRawSqlQuery<Product> result = db.Database.SqlQuery<Product>(sqlCommand.CommandText, sqlCommand.Parameters);
                Log.Debug(result.ToList().First());

                return View(result.AsEnumerable().First());
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }
    }
}