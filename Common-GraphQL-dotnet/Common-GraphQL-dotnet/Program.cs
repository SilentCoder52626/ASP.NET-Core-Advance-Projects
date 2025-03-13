using Common_GraphQL_dotnet.Query;
using Common_GraphQL_dotnet.Mutations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder
   .Services
   .AddGraphQLServer() // Adds a GraphQL server configuration to the DI
   .AddMutationType<GamesMutation>() // Add GraphQL root mutation type
   .AddQueryType<GamesQuery>() // Add GraphQL root query type
   .ModifyRequestOptions(options =>
   {
       // allow exceptions to be included in response when in development
       options.IncludeExceptionDetails = builder.Environment.IsDevelopment();
   });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
}

app.UseHttpsRedirection();


app.MapGraphQL("/");

app.Run();

