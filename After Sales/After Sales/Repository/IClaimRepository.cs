using ClientWebService.DTO;
using After_Sales.Model;

namespace ClientWebService.Repository
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetClaims();
        Task<Claim> GetClaim(int ClaimId );
        Task<IEnumerable<Claim>> GetClaimsByClient(string ClientEmail);
        Task<HttpResponseMessage> AddClaim(ClaimDto claim);

        Task<Client> getClientByEmail(string email);
        Task<HttpResponseMessage> updateClaim(int id, Claim updatedClaim);
    }
}
