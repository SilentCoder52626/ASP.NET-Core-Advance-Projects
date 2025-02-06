using Common_Response_Caching_MediatR_dotnet.Features.Products.DTOs;
using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Queries.Get
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto>;

}
