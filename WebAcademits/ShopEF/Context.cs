using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Model;

namespace ShopEF
{
    public class Context : DbContext
    {
        public Context() : base("StudyServer")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
    }
}
