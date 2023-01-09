namespace product_service.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }

        public string SparePartName { get; set; }   

        public string SparePartImage { get; set; }

        public float SparePartPrice  { get; set; }   

        public int ? ProductId { get; set; }

    }
}
