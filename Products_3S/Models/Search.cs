using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products_3S.Models
{
    public class Search
    {
        public string ProductName { get; set; }

        public int UnitID { get; set; }

        public int ReorderLevel { get; set; }

        public int SupplierID { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsOnOrder { get; set; }
    }
}