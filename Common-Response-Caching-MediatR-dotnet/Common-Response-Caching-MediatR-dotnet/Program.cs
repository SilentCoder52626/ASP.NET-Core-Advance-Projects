using Common_Response_Caching_MediatR_dotnet.Caching;
using Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Create;
using Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Delete;
using Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Update;
using Common_Response_Caching_MediatR_dotnet.Features.Products.Notifications;
using Common_Response_Caching_MediatR_dotnet.Features.Products.Queries.Get;
using Common_Response_Caching_MediatR_dotnet.Features.Products.Queries.List;
using Common_Response_Caching_MediatR_dotnet.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
});
builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.MapGet("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
    var product = await mediatr.Send(new GetProductQuery(id));
    if (product == null) return Results.NotFound();
    return Results.Ok(product);
});

app.MapGet("/products", async (ISender mediatr) =>
{
    var products = await mediatr.Send(new ListProductsQuery());
    return Results.Ok(products);
});

app.MapPost("/products", async (CreateProductCommand command, IMediator mediatr) =>
{
    var productId = await mediatr.Send(command);
    if (Guid.Empty == productId) return Results.BadRequest();
    await mediatr.Publish(new ProductCreatedNotification(productId));

    return Results.Created($"/products/{productId}", new { id = productId });
});

app.MapPut("/products/update", async (UpdateProductCommand command, ISender mediatr) =>
{
    await mediatr.Send(command);
    return Results.NoContent();
});

app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
    await mediatr.Send(new DeleteProductCommand(id));
    return Results.NoContent();
});


app.Run();

