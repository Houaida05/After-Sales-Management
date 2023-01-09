

namespace TechnicalSupportService.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }


        public float fees { get; set; }
        public bool Warranty { get; set; }
    }
}
