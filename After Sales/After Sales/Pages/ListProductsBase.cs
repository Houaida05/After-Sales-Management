using ClientWebService.DTO;
using After_Sales.Model;
using ClientWebService.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using After_Sales.Service;

namespace After_Sales.Pages
{
    public class ListProductsBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; } 
        [Inject]
        public IClaimRepository claimService { get; set; }
        [Inject]
        UserManager<IdentityUser> userManager { get; set; }

        

        public List<Product> Products { get; set; } = new List<Product>();
        
     
        [Inject]
        public NavigationManager NavigationManager { get; set; }

      
        public IEnumerable<SparePart> spareParts { get; set; } = new List<SparePart>();
        [Inject]
        public IProductService productService { get; set; }
        protected override async Task OnInitializedAsync()
        {


            var user = (await authenticationStateTask).User;
            if (!user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/Identity/Account/Login", true);

            }
            else
            {

                Products = (await productService.GetProducts()).ToList();
            }
        }
        protected async Task OnProductClick(int productId)
        {
            spareParts = await productService.GetSpareParts(productId);

        }
     
    }
}
