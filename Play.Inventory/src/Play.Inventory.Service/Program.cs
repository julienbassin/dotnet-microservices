using Microsoft.OpenApi.Models;
using Play.Common.MongoDB;
using Play.Common.Settings;
using Play.Inventory.Service;
using Play.Inventory.Service.Entities.Models;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

builder.Services.AddTransient<IExternalCatalogService, ExternalCatalogService>();

builder.Services.AddHttpClient<IExternalCatalogService, ExternalCatalogService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
}).AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(3));
builder.Services.AddMongo().AddMongoRepository<InventoryItem>("inventoryitems");
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Play.Inventory.Service", Version = "v1" });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Play.Inventory.Service v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

