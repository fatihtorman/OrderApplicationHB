using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;

namespace Repository
{
    public class OrderAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost; port=3306; database=order; user=root; password=fati; Persist Security Info=False; Connect Timeout=300";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public OrderAppDbContext(DbContextOptions<OrderAppDbContext> options) : base(options)
        {
        }
    }
}
