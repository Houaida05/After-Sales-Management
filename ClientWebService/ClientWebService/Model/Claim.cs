namespace ClientWebService.Model
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string Subject { get; set;  }
        public bool Warranty { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }  
        public int ProductId { get; set; }
    }
}
