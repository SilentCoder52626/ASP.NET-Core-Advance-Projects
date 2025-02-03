using MediatR;

namespace Common_Fluent_Validation_dotnet.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest;

}
