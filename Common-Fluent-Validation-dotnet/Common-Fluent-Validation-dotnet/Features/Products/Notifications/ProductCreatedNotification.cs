using MediatR;

namespace Common_Fluent_Validation_dotnet.Features.Products.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;

}
