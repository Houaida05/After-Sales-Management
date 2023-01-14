namespace ClientWebService.Model
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string Subject { get; set;  }
        public bool ClaimStatus { get; set; } = false;
        public int ClientId { get; set; }
        public Client? Client { get; set; }  
        public int ProductId { get; set; }
    }
}
