using Common_Fluent_Validation_dotnet.Features.Products.DTOs;
using MediatR;

namespace Common_Fluent_Validation_dotnet.Features.Products.Queries.List
{
    public record ListProductsQuery : IRequest<List<ProductDto>>;
}
