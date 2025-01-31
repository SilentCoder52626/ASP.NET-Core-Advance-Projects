using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest;

}
