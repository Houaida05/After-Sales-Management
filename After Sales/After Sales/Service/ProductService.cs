using After_Sales.Model;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.IO;
using System.IO.Pipelines;
using System.IO.Pipes;
using System.Security.Policy;

namespace After_Sales.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;
       
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateProduct(Product newProduct)
        {
            return await httpClient.PostAsJsonAsync<Product>("Product", newProduct);
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await httpClient.GetFromJsonAsync<Product>($"claims/product/{productId}");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await httpClient.GetFromJsonAsync<Product[]>("product");
        }

        public async Task<IEnumerable<SparePart>> GetSpareParts(int productId)
        {
            return await httpClient.GetFromJsonAsync<SparePart[]>($"spareParts/{productId}");

        }

        public async Task<HttpResponseMessage> uploadImage(MultipartFormDataContent content)
        {
            return await httpClient.PostAsync("images", content);
        }
     
    }
}
