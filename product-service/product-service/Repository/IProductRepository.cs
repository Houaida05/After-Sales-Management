using product_service.Models;
using shared;

namespace product_service
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int ProductId);
        Task<Product> AddProduct(Product Product);
        //Task<Product> UpdateProduct(Product Product);
        Task<Product> GetProductByProductName(string ProductName);
        Task<Product> DeleteProduct(int ProductId);

        Task<IEnumerable<SparePart>> GetSpareParts(int productId);


    }
}
