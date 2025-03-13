using Common_GraphQL_dotnet.Query;
using Common_GraphQL_dotnet.Mutations;
using Common_GraphQL_dotnet.DTO;
using Common_GraphQL_dotnet.Validator;
using FluentValidation;
using Common_GraphQL_dotnet.Error;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<AbstractValidator<GameReviewDto>, GameReviewValidator>();


builder
   .Services
   .AddGraphQLServer() // Adds a GraphQL server configuration to the DI
   .AddErrorFilter(provider =>
   {
       return new ServerErrorFilter(
          provider.GetRequiredService<ILogger<ServerErrorFilter>>(),
          builder.Environment);
   })
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

