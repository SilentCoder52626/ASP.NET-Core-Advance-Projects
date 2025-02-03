using MediatR;

namespace Common_Fluent_Validation_dotnet.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price) : IRequest;

}
