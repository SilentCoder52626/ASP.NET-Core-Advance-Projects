using Common_CQRS_Mediatr_dotnet.Features.Products.DTOs;
using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Queries.List
{
    public record ListProductsQuery : IRequest<List<ProductDto>>;
}
