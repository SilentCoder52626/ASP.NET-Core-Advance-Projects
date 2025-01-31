using Common_CQRS_Mediatr_dotnet.Features.Products.DTOs;
using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Queries.Get
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto>;

}
