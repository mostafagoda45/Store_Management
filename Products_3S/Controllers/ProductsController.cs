using log4net;
using Products_3S.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Products_3S.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductsController));
        private ProductDBContext db = new ProductDBContext(); 

        public ActionResult Index()
        {
            Log.Debug("Attemp to get all products.....");
            var result = db.Products;
            Log.Debug(string.Format("Product list fetched successfully : {0}", result));
            return View(result.ToList());
        }

        public ActionResult Create()
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
        public ActionResult Create([Bind(Exclude = "ProductID")]Product product)
        {
            Log.Debug(string.Format("Attemp to save new product : {0}", product));
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                try
                {
                    db.SaveChanges();
                    Log.Debug("Successfully save product");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Failed to save product", ex.Message));
                    return new HttpStatusCodeResult(500, ex.Message);
                } 
            }
            else
            {
                ProductUnitModelView model = new ProductUnitModelView()
                {
                    Product = product,
                    Units = db.Units.Select(i => new SelectListItem { Value = i.ID.ToString(), Text = i.UnitName }),
                    Suppliers = db.Suppliers.Select(i => new SelectListItem { Value = i.SupplierID.ToString(), Text = i.SupplierName })
                };
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            ProductUnitModelView model = new ProductUnitModelView()
            {
                Product = db.Products.SingleOrDefault(p => p.ProductID == id),
                Units = db.Units.Select(i => new SelectListItem { Value = i.ID.ToString(), Text = i.UnitName }),
                Suppliers = db.Suppliers.Select(i => new SelectListItem { Value = i.SupplierID.ToString(), Text = i.SupplierName })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Exclude = "ProductID")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = db.Products.SingleOrDefault(p => p.ProductID == id);
                    result.ProductName = product.ProductName;
                    result.QuantityPerUnit = product.QuantityPerUnit;
                    result.ReorderLevel = product.ReorderLevel;
                    result.SupplierID = product.SupplierID;
                    result.UnitPrice = product.UnitPrice;
                    result.UnitsInStock = product.UnitsInStock;
                    result.UnitsOnOrder = product.UnitsOnOrder;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                ProductUnitModelView model = new ProductUnitModelView()
                {
                    Product = product,
                    Units = db.Units.Select(i => new SelectListItem { Value = i.ID.ToString(), Text = i.UnitName }),
                    Suppliers = db.Suppliers.Select(i => new SelectListItem { Value = i.SupplierID.ToString(), Text = i.SupplierName })
                };
                return View(model);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            try
            {
                var result = db.Products.FirstOrDefault(p => p.ProductID == id);
                return View(result);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(db.Products.SingleOrDefault(s => s.ProductID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            try
            {
                var result = db.Products.FirstOrDefault(s => s.ProductID == id);
                db.Products.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}