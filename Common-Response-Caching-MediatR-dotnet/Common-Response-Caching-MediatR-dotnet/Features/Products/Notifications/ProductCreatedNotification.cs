using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;

}
