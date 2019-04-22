using Products_3S.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Products_3S.Controllers
{
    public class SuppliersController : Controller
    {
        private ProductDBContext db = new ProductDBContext();
        public ActionResult Index()
        {
            var result = db.Suppliers;
            return View(result.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "SupplierID")]Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                return View(supplier);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(db.Suppliers.SingleOrDefault(s => s.SupplierID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "SupplierName")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = db.Suppliers.SingleOrDefault(s => s.SupplierID == id);
                    result.SupplierName = supplier.SupplierName;
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
                return View(supplier);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(db.Suppliers.SingleOrDefault(s => s.SupplierID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            try
            {
                var result = db.Suppliers.FirstOrDefault(s => s.SupplierID == id);
                if(result.Products != null)
                {
                    result.Products.ToList().ForEach(i => db.Products.Remove(i));
                }
                db.Suppliers.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
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
                var result = db.Suppliers.FirstOrDefault(s => s.SupplierID == id);
                return View(result);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }  
    }
}
