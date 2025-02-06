﻿using Common_Response_Caching_MediatR_dotnet.Persistence;
using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.Id);
            if (product == null) return;
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
