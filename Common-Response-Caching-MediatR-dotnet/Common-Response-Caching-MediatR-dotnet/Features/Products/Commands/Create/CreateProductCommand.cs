using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Create
{
    public record CreateProductCommand(string Name, string Description, decimal Price) : IRequest<Guid>;

}
