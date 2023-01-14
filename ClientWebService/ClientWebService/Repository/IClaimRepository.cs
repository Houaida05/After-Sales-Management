using ClientWebService.DTO;
using ClientWebService.Model;

namespace ClientWebService.Repository
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetClaims();
        Task<Claim> GetClaim(int ClaimId );
        Task<IEnumerable<Claim>> GetClaimsByClient( string ClientName);
        Task<Claim> AddClaim(ClaimDto claim);
        Task<Claim> UpdateClaim(Claim claim);
        Task<Client> getClientByEmail(string email);

        Task<Product> getProductClaim(int productId);
    }
}
