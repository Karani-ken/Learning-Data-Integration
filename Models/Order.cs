using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Data_Integration.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public int customerId { get; set; }
        public Customer customer { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
