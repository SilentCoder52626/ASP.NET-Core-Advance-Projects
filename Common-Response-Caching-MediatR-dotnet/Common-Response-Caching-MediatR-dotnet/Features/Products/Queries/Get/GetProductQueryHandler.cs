using Common_Response_Caching_MediatR_dotnet.Features.Products.DTOs;
using Common_Response_Caching_MediatR_dotnet.Persistence;
using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Queries.Get
{
    public class GetProductQueryHandler(AppDbContext context)
     : IRequestHandler<GetProductQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.Id);
            if (product == null)
            {
                return null;
            }
            return new ProductDto(product.Id, product.Name, product.Description, product.Price);
        }
    }
}
