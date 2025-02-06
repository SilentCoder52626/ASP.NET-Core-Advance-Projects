using MediatR;

namespace Common_Response_Caching_MediatR_dotnet.Features.Products.Notifications
{
    public class StockAssignedHandler(ILogger<StockAssignedHandler> logger) : INotificationHandler<ProductCreatedNotification>
    {
        public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"handling notification for product creation with id : {notification.Id}. assigning stocks.");
            return Task.CompletedTask;
        }
    }
}
