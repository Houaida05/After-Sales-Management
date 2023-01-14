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
    public class CreateClaimBase : ComponentBase
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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string selectedValue;

        protected override async Task OnInitializedAsync()
        {
            

            ClaimDto = new ClaimDto();
            ClaimDto.claim = new Claim();
            ClaimDto.client = new Client();
            Products = (await productService.GetProducts()).ToList();

            var user = (await authenticationStateTask).User;
            if (!user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/Identity/Account/Login", true);
            }
        }
        protected async Task HandleValidSubmit()
        {
            var user = (await authenticationStateTask).User;

            
            if (user.Identity.IsAuthenticated)
            {
                var currentUser = await userManager.GetUserAsync(user);
                ClaimDto.client.FirstName = currentUser.Email.Split('@')[0];
               
                ClaimDto.client.Email = currentUser.Email;
               

                Console.WriteLine(JsonSerializer.Serialize(ClaimDto));
                var result = await claimService.AddClaim(ClaimDto);
            if(result != null)
                {
                    NavigationManager.NavigateTo("/ListClaims");
    }
           }
            else
            {
                NavigationManager.NavigateTo("/Identity/Account/Login", true);

            }
            
        }
        protected void OnChangeProduct(string value)
        {
            //do something
            selectedValue = "Selected Value: " + value;
        }
    }
}
