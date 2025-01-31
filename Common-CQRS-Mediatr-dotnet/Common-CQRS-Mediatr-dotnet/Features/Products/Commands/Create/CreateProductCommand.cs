using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Commands.Create
{
    public record CreateProductCommand(string Name, string Description, decimal Price) : IRequest<Guid>;

}
