using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest;

}
