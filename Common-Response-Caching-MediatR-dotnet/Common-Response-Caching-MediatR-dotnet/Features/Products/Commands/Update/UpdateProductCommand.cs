using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price) : IRequest;

}
