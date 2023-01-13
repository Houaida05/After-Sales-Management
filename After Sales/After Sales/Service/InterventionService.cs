using ClientWebService.DTO;
using After_Sales.Model;
using After_Sales.Repository;

namespace After_Sales.Service
{
    public class InterventionService : IInterventionRepository
    {
        private readonly HttpClient httpClient;

        public InterventionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
      

        public async Task<HttpResponseMessage> AddIntervention(Intervention intervention)
        {
            return await httpClient.PostAsJsonAsync<Intervention>("intervention", intervention);

        }
    }
}
