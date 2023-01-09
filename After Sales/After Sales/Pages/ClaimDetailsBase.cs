using After_Sales.Model;
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

        public Product product { get; set; } = new Product();
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
            }
        }
       
    }
}
