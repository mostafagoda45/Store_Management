using System.Collections.Generic;
using System.Web.Mvc;

namespace Products_3S.Models
{
    public class ProductUnitModelView
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }
    }
}