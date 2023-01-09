
using MassTransit;

using System.Text.Json;
using shared;
using ClientWebService.DB;
using ClientWebService.Model;

namespace ClientWebService.Consumer
{
    public class ProductToBeCreatedConsumer : IConsumer<shared.ProductCreated>
    {
        private readonly AppDbContext _appDbContext;
    

        public ProductToBeCreatedConsumer(AppDbContext appDbContext)
        {
            
            _appDbContext = appDbContext; 
        }

        public async Task Consume(ConsumeContext<shared.ProductCreated> context)
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
