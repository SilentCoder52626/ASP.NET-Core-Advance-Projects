﻿using Common_Response_Caching_MediatR_dotnet.Features.Products.DTOs;
using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Queries.List
{
    public record ListProductsQuery : IRequest<List<ProductDto>>;
}
