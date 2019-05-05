using log4net;
using Products_3S.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
                Products = new List<Product>(),
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
                List<object> param = new List<object>();
                foreach(PropertyInfo property in product.GetType().GetProperties().ToList())
                {
                    if(!property.Name.Equals("ProductID") && !typeof(Supplier).IsAssignableFrom(property.PropertyType) && !typeof(Unit).IsAssignableFrom(property.PropertyType))
                    {
                        if (!(property.GetValue(product, null) == null))
                        {
                            if (typeof(string).IsAssignableFrom(property.PropertyType))
                            {
                                param.Add(string.Format("@{0} = '{1}'", property.Name, property.GetValue(product, null)));
                            }
                            else
                            {
                                param.Add(string.Format("@{0} = {1}", property.Name, property.GetValue(product, null)));
                            }
                        }
                        else
                        {
                            param.Add(string.Format("@{0} = null", property.Name));
                        }
                    }
                }
               
                string query = string.Format("exec [dbo].[Products_search] {0}", string.Join(",", param.ToArray()));

                var result = db.Database.SqlQuery<Product>(query).ToList();
                foreach(var i in result)
                {
                    i.Unit = db.Units.FirstOrDefault(u => u.ID == i.QuantityPerUnit);
                    i.Supplier = db.Suppliers.FirstOrDefault(s => s.SupplierID == i.SupplierID);
                }
                ProductUnitModelView model = new ProductUnitModelView()
                {
                    Product = product,
                    Products = result,
                    Units = db.Units.Select(i => new SelectListItem { Value = i.ID.ToString(), Text = i.UnitName }),
                    Suppliers = db.Suppliers.Select(i => new SelectListItem { Value = i.SupplierID.ToString(), Text = i.SupplierName })
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}