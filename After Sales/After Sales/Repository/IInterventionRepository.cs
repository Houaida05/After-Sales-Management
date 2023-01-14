using After_Sales.Model;

namespace After_Sales.Repository
{
    public interface IInterventionRepository
    {
        Task<HttpResponseMessage> AddIntervention(Intervention intervention);
        Task<Intervention> GetIntervention(int interventionId);

    }

}
