

namespace Learning_Data_Integration.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
