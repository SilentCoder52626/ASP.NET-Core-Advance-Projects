
using Common_Response_Caching_MediatR_dotnet.Persistence;
using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.Id);
            if (product == null) throw new Exception("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
