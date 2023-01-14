
using Microsoft.EntityFrameworkCore;
using TechnicalSupportService.DB;
using TechnicalSupportService.Models;
using TechnicalSupportService.Repository;

namespace TechnicalSupportService.Service
{
    public class InterventionService : IInterventionRepository
    {
        private readonly DBContext appDbContext;
        public InterventionService(DBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Intervention> AddIntervention(Intervention Intervention)
        {
            var result = await appDbContext.interventions.AddAsync(Intervention);
            await appDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Intervention> GetIntervention(int id)
        {
            return await appDbContext.interventions.FirstOrDefaultAsync(i => i.ClaimId == id);
        }

        public Task<IEnumerable<Intervention>> GetInterventions()
        {
            throw new NotImplementedException();
        }
    }

}