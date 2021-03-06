﻿using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Products_3S.Models
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }

        public ProductDBContext() : base ("DatabaseConnection")
        {

        }
    }
}