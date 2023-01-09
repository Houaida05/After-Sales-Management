
using Microsoft.EntityFrameworkCore;
using product_service.DB;
using product_service.Models;
using shared;

namespace product_service.Service
{
    public class ProductService: IProductRepository
    {
        private readonly DBContext appDbContext;
        public ProductService(DBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await appDbContext.Products.Include(p => p.SpareParts).ToListAsync();
        }
        public async Task<Product> GetProduct(int ProductId)
        {
            return await appDbContext.Products.Include(p => p.SpareParts)
                .FirstOrDefaultAsync(p => p.ProductId == ProductId);
        }
        public async Task<Product> AddProduct(Product Product)
        {
            var result = await appDbContext.Products.AddAsync(Product);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

     
        public async Task<Product> DeleteProduct(int ProductId)
        {
            var result = await appDbContext.Products
                            .FirstOrDefaultAsync(e => e.ProductId == ProductId);
            if (result != null)
            {
                appDbContext.Products.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;

        }

   

        public async Task<Product> GetProductByProductName(string ProductName)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(p => p.ProductName == ProductName);
        }

        public async Task<IEnumerable<SparePart>> GetSpareParts(int productId)
        {
            return await appDbContext.SpareParts.Where(s => s.ProductId == productId).ToListAsync();
        
        }

    }
}
//public async Task<Product> UpdateProduct(Product Product)
//{
//    var result = await appDbContext.Products
//        .FirstOrDefaultAsync(e => e.ProductId == Product.ProductId);
//    if (result != null)
//    {
//        result.FirstName = Product.FirstName;
//        result.LastName = Product.LastName;
//        result.Email = Product.Email;
//        result.DateOfBirth = Product.DateOfBirth;
//        result.Gender = Product.Gender;
//        result.DepartmentId = Product.DepartmentId;
//        result.PhotoPath = Product.PhotoPath;
//        await appDbContext.SaveChangesAsync();
//        return result;
//    }
//    return null;
//}