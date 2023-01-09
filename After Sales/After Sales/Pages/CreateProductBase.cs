using After_Sales.Model;
using After_Sales.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;


namespace After_Sales.Pages
{
    public class CreateProductBase : ComponentBase
    {
        private int maxAllowedFiles = 1;
        private long maxFileSize = long.MaxValue;
        public List<string> fileNames = new();
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Product Product { get; set; } = new Product();
        protected override async Task OnInitializedAsync()
        {
            Product = new Product();
        }
            protected async Task OnValidSubmit()
        {
            Product.ProductPhoto = fileNames[0];
            for(var i = 0; i < Product.SpareParts.Count; i++)
        {
                var sparepart = Product.SpareParts.ElementAt(i);

                sparepart.SparePartImage = fileNames.ElementAt(i + 1);
            }
            var response = await ProductService.CreateProduct(Product);

            if (response!=null)
            {
                NavigationManager.NavigateTo("ListProducts");
            }
           
        }
 
        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            using var content = new MultipartFormDataContent();

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                fileNames.Add(file.Name);

                content.Add(
                    content: fileContent,
                    name: "\"image\"",
                    fileName: file.Name);
            }

            var response = await ProductService.uploadImage(content);

            if (response.IsSuccessStatusCode)
            {
                var uploadedFileName = await response.Content.ReadAsStringAsync();
            }
        }
        protected void Clear()
        {
            Product = new Product();
        }
       

    }
}
