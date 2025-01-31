using MediatR;

namespace Common_CQRS_Mediatr_dotnet.Features.Products.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;

}
