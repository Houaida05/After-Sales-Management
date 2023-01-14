using ClientWebService.DTO;
using After_Sales.Model;
using ClientWebService.Repository;


namespace After_Sales.Service
{
    public class ClaimService : IClaimRepository
    {
        private readonly HttpClient httpClient;

        public ClaimService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> AddClaim(ClaimDto claim)
        {
            return await httpClient.PostAsJsonAsync<ClaimDto>("Claim", claim);
        }

        public async Task<Claim> GetClaim(int ClaimId)
        {
            return await httpClient.GetFromJsonAsync<Claim>($"Claim/{ClaimId}");

        }

        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return await httpClient.GetFromJsonAsync<Claim[]>("Claim");
        }

        public async Task<IEnumerable<Claim>> GetClaimsByClient(string ClientName)
        {
            return await httpClient.GetFromJsonAsync<Claim[]>($"claims/{ClientName}");
        }

        public Task<Client> getClientByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> updateClaim(int id, Claim updatedClaim)
        {
            return await httpClient.PutAsJsonAsync<Claim>($"Claim/{id}", updatedClaim);

        }
    }
}
