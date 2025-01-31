using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price) : IRequest;

}
