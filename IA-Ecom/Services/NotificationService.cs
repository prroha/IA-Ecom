using IA_Ecom.ViewModels;

namespace IA_Ecom.Services;

public class NotificationService : INotificationService
{
    private readonly List<NotificationMessage> _notifications = new List<NotificationMessage>();

    public void AddNotification(string message, NotificationType type)
    {
        _notifications.Add(new NotificationMessage { Message = message, Type = type });
    }

    public IEnumerable<NotificationMessage> GetNotifications()
    {
        var notifications = new List<NotificationMessage>(_notifications);
        ClearNotifications();
        return notifications;
    }

    public void ClearNotifications()
    {
        _notifications.Clear();
    }
}
