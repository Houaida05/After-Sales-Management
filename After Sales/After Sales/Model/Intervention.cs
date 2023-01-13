  

namespace After_Sales.Model
{
    public class Intervention
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public int SparePartId { get; set; }
        public float fees { get; set; }
        public bool Warranty { get; set; }
    }
}
