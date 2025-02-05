using Common_InMemoryCaching_dotnet.DTOs;
using Common_InMemoryCaching_dotnet.Models;

namespace Common_InMemoryCaching_dotnet.Services
{
    public interface IProductService
    {
        Task<Product> Get(Guid id);
        Task<List<Product>> GetAll();
        Task Add(ProductCreationDto product);
    }
}
