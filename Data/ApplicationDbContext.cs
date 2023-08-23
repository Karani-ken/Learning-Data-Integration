

using Learning_Data_Integration.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning_Data_Integration.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-VF9PAJV;Database=myShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)//an order has many products
                .WithMany(p => p.Orders)//a product can be in many orders
                .UsingEntity(join => join.ToTable("OrderProducts"));

            modelBuilder.Entity<Order>()
                .HasOne(o => o.customer) /// an order has one customer
                .WithMany(c => c.Orders)//a customer has many orders
                .HasForeignKey(o => o.customerId);//maps the order to a certain customer

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders) //a customer can have many orders
                .WithOne(o => o.customer) //an order can only belong to one customer
                .HasForeignKey(o => o.customerId);//
        }
    }
}
