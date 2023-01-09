using ClientWebService.DTO;
using After_Sales.Model;
using ClientWebService.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using After_Sales.Service;

namespace After_Sales.Pages
{
    public class LisClaimsBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; } 
        [Inject]
        public IClaimRepository claimService { get; set; }
        [Inject]
        UserManager<IdentityUser> userManager { get; set; }

        [Inject]
        public IProductService productService { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public string ProductId { get; set; }
        public ClaimDto ClaimDto { get; set; } = new ClaimDto();
        public List<Claim> claims { get; set; } = new List<Claim>();    

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string selectedValue;

        protected override async Task OnInitializedAsync()
        {


            var user = (await authenticationStateTask).User;
            if (!user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/Identity/Account/Login", true);

            }
            else
            {

                claims = (await claimService.GetClaimsByClient(user.Identity.Name)).ToList();
            }
        }
        protected void OnChangeProduct(string value)
        {
            //do something
            selectedValue = "Selected Value: " + value;
        }
    }
}
