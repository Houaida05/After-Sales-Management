

using After_Sales.Model;

namespace After_Sales.Service
{
    public interface IProductService
    {
          Task<HttpResponseMessage> CreateProduct(Product newProduct);
        Task<Product> GetProduct(int productId);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<SparePart>> GetSpareParts(int productId);
        Task<HttpResponseMessage> uploadImage(MultipartFormDataContent content);

    }
}
