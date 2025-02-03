using Common_Fluent_Validation_dotnet.Behaviors;
using Common_Fluent_Validation_dotnet.Features.Products.Commands.Create;
using Common_Fluent_Validation_dotnet.Features.Products.Commands.Delete;
using Common_Fluent_Validation_dotnet.Features.Products.Commands.Update;
using Common_Fluent_Validation_dotnet.Features.Products.Notifications;
using Common_Fluent_Validation_dotnet.Features.Products.Queries.Get;
using Common_Fluent_Validation_dotnet.Features.Products.Queries.List;
using Common_Fluent_Validation_dotnet.Models;
using Common_Fluent_Validation_dotnet.Persistence;
using FluentValidation;
using MediatR;
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
    cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));

});

//builder.Services.AddScoped<IValidator<UserRegistrationRequest>, UserRegistrationValidator>(); // for Single class


builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //for Whole assembly


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/register", async (UserRegistrationRequest request, IValidator<UserRegistrationRequest> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    // perform actual service call to register the user to the system
    // _service.RegisterUser(request);
    return Results.Accepted();
});

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

