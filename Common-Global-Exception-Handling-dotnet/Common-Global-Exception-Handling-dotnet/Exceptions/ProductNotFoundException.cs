using System.Net;

namespace Common_Global_Exception_Handling_dotnet.Exceptions
{
    public class ProductNotFoundException : BaseException
    {
        public ProductNotFoundException(Guid id)
        : base($"product with id {id} not found", HttpStatusCode.NotFound)
        {
        }
    }
}
