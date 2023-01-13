
using MassTransit;

using System.Text.Json;
using Shared;
using TechnicalSupportService.DB;
using TechnicalSupportService.Models;

namespace TechnicalSupportService.Consumer
{
    public class ProductToBeCreatedConsumer : IConsumer<Shared.ProductCreated>
    {
        private readonly DBContext _appDbContext;
    

        public ProductToBeCreatedConsumer(DBContext appDbContext)
        {
            
            _appDbContext = appDbContext; 
        }

        public async Task Consume(ConsumeContext<Shared.ProductCreated> context)
        {

            var newProduct = new Product
            {
                //ProductId = context.Message.Id,
                ProductName = context.Message.Name,
                ProductPhoto = context.Message.Photo,
            };
            
            _appDbContext.Products.Add(newProduct);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
