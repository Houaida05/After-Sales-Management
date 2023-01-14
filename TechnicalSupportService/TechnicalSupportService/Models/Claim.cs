using System.ComponentModel.DataAnnotations;

namespace TechnicalSupportService.Models
{
    public class Claim
    {

        [Key]
        public int ClaimId { get; set; }
        public string Subject { get; set;  }
       



    }
}
