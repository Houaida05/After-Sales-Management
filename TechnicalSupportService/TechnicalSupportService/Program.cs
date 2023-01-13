using MassTransit;
using Microsoft.EntityFrameworkCore;
using TechnicalSupportService.Consumer;
using TechnicalSupportService.DB;
using TechnicalSupportService.Repository;
using TechnicalSupportService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInterventionRepository, InterventionService>();
builder.Services.AddDbContext<DBContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<ProductToBeCreatedConsumer>();
   options.AddConsumer<ClaimCreatedConsumer>();
    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost:4001"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("event-listener-product-tech-support-service", e =>
        {
            e.ConfigureConsumer<ProductToBeCreatedConsumer>(context);
            //e.ConfigureConsumer<ClaimCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("event-listener-claim-tech-support-service", e =>
        {
            e.ConfigureConsumer<ClaimCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
