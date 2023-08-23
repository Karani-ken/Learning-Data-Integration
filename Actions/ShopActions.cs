using Learning_Data_Integration.Data;
using Learning_Data_Integration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Data_Integration.Actions
{
    public class ShopActions
    {
        ApplicationDbContext _context = new ApplicationDbContext();
       

         
        public void AddData()
        {
             //adding products
         var product1 = new Product{ProductName= "Bread", Price=65};
         var product2 = new Product{ProductName= "Laptop", Price=40000};
         var product3 = new Product{ProductName= "Pen", Price=20};
          var product4 = new Product{ProductName= "Car", Price=2000};
         var product5 = new Product{ProductName= "Book", Price=200};
          _context.Products.Add(product1);
         _context.Products.Add(product2);
         _context.Products.Add(product3);
         _context.Products.Add(product4);
         _context.Products.Add(product5);
          _context.SaveChanges();
            //adding customers
         var customer1 = new Customer{Name= "Jack"};
         var customer2 = new Customer{Name="Leah"};
         var customer3 = new Customer{Name="Wesley"}; 
         var customer4 = new Customer{Name="James"};
         var customer5 = new Customer{Name="Emily"};
        _context.Customers.Add(customer1);
        _context.Customers.Add(customer2);
        _context.Customers.Add(customer3);
        _context.Customers.Add(customer4);
        _context.Customers.Add(customer5);
        _context.SaveChanges();
         //adding orders
         var Order1 = new Order { customerId = customer1.Id, Products = { product1, product3 } };
         var Order2 = new Order { customerId = customer2.Id, Products = { product1, product2 } };
         var Order3 = new Order { customerId = customer3.Id, Products = { product2, product3 } };
         _context.Orders.Add(Order1);
        _context.Orders.Add(Order2);
        _context.Orders.Add(Order3);
         _context.SaveChanges();
        }
        //inner join
        public async Task GetOrders()
        {
            var AllOrders = await _context.Orders.Include(o => o.Products).Include(o => o.customer).ToListAsync();
            foreach(var order in AllOrders)
            {
                Console.WriteLine($"{order.orderId}. Customer name {order.customer.Name}");
                foreach(var product in order.Products)
                {
                    await Console.Out.WriteLineAsync($"{product.ProductName}");
                }
            }
        }
        //left join
        public async Task GetCustomers()
        {
            var AllCustomers = await _context.Customers.Include(c => c.Orders).ToListAsync();
             foreach(var customer in AllCustomers)
            {
                await Console.Out.WriteLineAsync($"{customer.Name}");
                foreach(var order in customer.Orders)
                {
                    await Console.Out.WriteLineAsync($"{order.orderId}");
                   
                }
            }
        }
        //Right join
        public async Task GetProducts()
        {
            var AllProducts = await _context.Products.Include(p => p.Orders).ToListAsync();
            foreach(var product in AllProducts)
            {
                await Console.Out.WriteLineAsync($"name:{product.ProductName}, price:{product.Price}");
                foreach(var order in product.Orders)
                {
                    await Console.Out.WriteLineAsync($"Id: {order.orderId}");
                }
            }
        }
        //Full outer Join
        public async Task GetEverything()
        {
            var UnOrderedProducts = await _context.Products.Where(p => p.Orders.Count == 0).ToListAsync();
            var Everything = await _context.Customers.Include(c => c.Orders)
                .ThenInclude(Orders => Orders.Products).ToListAsync();
            foreach(var item in Everything)
            {
                await Console.Out.WriteLineAsync($"{item.Name}");
                foreach(var order in item.Orders)
                {
                    await Console.Out.WriteLineAsync($"id:{order.orderId}");
                    foreach(var product in order.Products)
                    {
                        await Console.Out.WriteLineAsync($"{product.ProductName} price:{product.Price}");
                    }
                }
               

            }
            foreach(var product in UnOrderedProducts)
            {
                await Console.Out.WriteLineAsync($"{product.ProductName} price: {product.Price}");
            }
           
              
        }
    }
}
