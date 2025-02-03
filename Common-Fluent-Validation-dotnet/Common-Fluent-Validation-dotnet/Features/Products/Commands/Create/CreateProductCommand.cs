using MediatR;

namespace Common_Fluent_Validation_dotnet.Features.Products.Commands.Create
{
    public record CreateProductCommand(string Name, string Description, decimal Price) : IRequest<Guid>;

}
