

using TechnicalSupportService.Models;

namespace TechnicalSupportService.Repository
{
    public interface IInterventionRepository
    {
        Task<IEnumerable<Intervention>> GetInterventions();
        Task<Intervention> GetIntervention(int InterventionId);
        Task<Intervention> AddIntervention(Intervention Intervention);
     

       


    }
}
