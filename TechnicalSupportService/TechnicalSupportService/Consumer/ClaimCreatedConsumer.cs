
using MassTransit;

using System.Text.Json;
using Shared;
using TechnicalSupportService.DB;
using TechnicalSupportService.Models;

namespace TechnicalSupportService.Consumer
{
    public class ClaimCreatedConsumer : IConsumer<Shared.ClaimCreated>
    {
        private readonly DBContext _appDbContext;
    

        public ClaimCreatedConsumer(DBContext appDbContext)
        {
            
            _appDbContext = appDbContext; 
        }

        public async Task Consume(ConsumeContext<Shared.ClaimCreated> context)
        {

            var newClaim = new Claim
            {
                //ProductId = context.Message.Id,
                //ProductName = context.Message.Name,
                Subject = context.Message.Subject,
            };
            
            _appDbContext.claims.Add(newClaim);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
