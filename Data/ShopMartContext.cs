using ShopMart.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopMart.Data
{
    public class ShopMartContext : DbContext
    {
        public ShopMartContext(DbContextOptions<ShopMartContext> options) : base(options)
        {
        }

        public DbSet<CardDetail> CardDetails { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustumerAddresses { get; set; }
        public DbSet<CustomerPhone> CustomerPhone { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
         public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
    }
}