namespace After_Sales.Model
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string Subject { get; set;  }
        public bool ClaimStatus { get; set; }
        public int ClientId { get; set; }

        
        public Client? Client { get; set; }  

        public int ProductId { get; set; }  
    }
}
