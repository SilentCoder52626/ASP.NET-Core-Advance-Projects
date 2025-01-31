namespace Common_CQRS_Mediatr_dotnet.Features.Products.DTOs
{
    public record ProductDto(Guid Id, string Name, string Description, decimal Price);
}
