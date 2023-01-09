namespace After_Sales.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 

        public string ProductPhoto { get; set; }
        public float ProductPrice { get; set; }

        public ICollection<SparePart> SpareParts { get; set; }=new List<SparePart>();
    }
}
