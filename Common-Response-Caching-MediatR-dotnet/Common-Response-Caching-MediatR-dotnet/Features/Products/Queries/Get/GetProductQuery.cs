using Common_Response_Caching_MediatR_dotnet.Caching;
using Common_Response_Caching_MediatR_dotnet.Features.Products.DTOs;
using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Queries.Get
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto>, ICacheable
    {
        public bool BypassCache => false;

        public string CacheKey => $"product:{Id}";

        public int SlidingExpirationInMinutes => 30;

        public int AbsoluteExpirationInMinutes => 60;
    }
}
