﻿using Common_CQRS_Mediatr_dotnet.Features.Products.DTOs;
using Common_CQRS_Mediatr_dotnet.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Queries.List
{
    public class ListProductsQueryHandler(AppDbContext context) : IRequestHandler<ListProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            return await context.Products
                .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price))
                .ToListAsync();
        }
    }
}
