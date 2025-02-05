using Common_InMemoryCaching_dotnet.DTOs;
using Common_InMemoryCaching_dotnet.Persistence;
using Common_InMemoryCaching_dotnet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("dotnetSeries"));
});
builder.Services.AddMemoryCache();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/products", async (IProductService service) =>
{
    var products = await service.GetAll();
    return Results.Ok(products);
});

app.MapGet("/products/{id:guid}", async (Guid id, IProductService service) =>
{
    var product = await service.Get(id);
    return Results.Ok(product);
});

app.MapPost("/products", async (ProductCreationDto product, IProductService service) =>
{
    await service.Add(product);
    return Results.Created();
});

app.Run();
