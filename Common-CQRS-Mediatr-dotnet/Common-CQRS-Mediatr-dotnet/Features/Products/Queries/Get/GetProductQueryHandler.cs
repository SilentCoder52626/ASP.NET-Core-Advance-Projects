using Common_CQRS_Mediatr_dotnet.Features.Products.DTOs;
using Common_CQRS_Mediatr_dotnet.Persistence;
using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Queries.Get
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
