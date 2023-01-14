using ClientWebService.DB;
using ClientWebService.DTO;
using ClientWebService.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientWebService.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext appDbContext;

        public ClaimRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;   
        }
        public async Task<Claim> AddClaim(ClaimDto ClaimDto)
        {
            var client = await getClientByEmail(ClaimDto.client.Email);
            if(client == null)
            {
                await appDbContext.Clients.AddAsync(ClaimDto.client);
                await appDbContext.SaveChangesAsync();
                ClaimDto.claim.ClientId = ClaimDto.client.ClientId;
            }
            else
            {
                ClaimDto.claim.ClientId = client.ClientId;
            }

            var result = await appDbContext.Claims.AddAsync(ClaimDto.claim);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Claim> GetClaim(int claimId)
        {
            return await appDbContext.Claims.Include(c => c.Client).
                           FirstOrDefaultAsync(c => c.ClaimId == claimId);
        }

        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return await appDbContext.Claims.Include(c => c.Client).ToListAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsByClient(string ClientEmail)
        {
            return await appDbContext.Claims.Include(c=>c.Client).Where(c => c.Client.Email == ClientEmail).ToListAsync();
        }

        public async Task<Client> getClientByEmail(string email)
        {
            return await appDbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Product> getProductClaim(int productId)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId== productId);
        }

        public async Task<Claim> UpdateClaim(Claim claim)
        {
            var claim1 =await appDbContext.Claims.FirstOrDefaultAsync(c => c.ClaimId == claim.ClaimId);
            if (claim1 != null)
            {
                claim1.ClaimStatus= claim.ClaimStatus;
                await appDbContext.SaveChangesAsync();

            }
            return claim1;
        }
    }
}
