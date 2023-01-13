using After_Sales.Model;
using After_Sales.Repository;
using After_Sales.Service;
using ClientWebService.DTO;
using ClientWebService.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Text.Json;

namespace After_Sales.Pages
{
    public class ClaimDetailsBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; } 
      
        [Inject]
        UserManager<IdentityUser> userManager { get; set; }

        [Inject]
        public IProductService productService { get; set; }
        

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }
        public Claim claim { get; set; } = new Claim();
        [Inject]
        public IClaimRepository claimService { get; set; }
        [Inject]
        public IInterventionRepository interventionService { get; set; }
        public Product product { get; set; } = new Product();
        public IEnumerable<SparePart> SpareParts { get; set; } = new List<SparePart>();
        public Intervention intervention { get; set;} = new Intervention();
        public float feeIntervention { get; set; }
        public SparePart sparePart = new SparePart();
        protected override async Task OnInitializedAsync()
        {            
            var user = (await authenticationStateTask).User;
            if (!user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/Identity/Account/Login", true);
            }
            else
            {
                Id = Id ?? "1";
                claim = await claimService.GetClaim(int.Parse(Id));
                product = await productService.GetProduct(claim.ProductId);

                SpareParts = await productService.GetSpareParts(product.ProductId);
                intervention.fees = 0;
            }
        }

        protected async void createIntervention()
        {
            intervention = new Intervention();
            
            intervention.ClaimId = claim.ClaimId;
            
            var result = await interventionService.AddIntervention(intervention);
            Console.WriteLine(JsonSerializer.Serialize(intervention));

            if (result != null)
            {
                NavigationManager.NavigateTo("/ListProducts");
            }
        
            else
            {
                NavigationManager.NavigateTo("/Identity/Account/Login", true);

            }
}
        protected string getLabel(bool val)
        {
            return !val ? "No Warranty" : "Warranty";
        }
        protected async void  GetTotalFee(int sparePartId)
        {
            
            sparePart = await productService.GetSparePart(sparePartId);
            if (getLabel(intervention.Warranty) == "Warranty")
            {

                intervention.Warranty = true;
                intervention.fees = 0;
                feeIntervention = 0;
                Console.WriteLine(JsonSerializer.Serialize(feeIntervention));
            }
            else
            {
                feeIntervention = sparePart.SparePartPrice +20;
                intervention.Warranty = false;
                intervention.fees = feeIntervention;
                Console.WriteLine(JsonSerializer.Serialize(feeIntervention));
            }
        }
    }
}
