using Shared;

namespace product_service.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 

        public string ProductPhoto { get; set; }
        public float ProductPrice { get; set; }

        public ICollection<SparePart> SpareParts { get; set; }
    }
}
