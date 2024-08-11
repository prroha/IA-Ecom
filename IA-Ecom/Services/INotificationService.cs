using IA_Ecom.ViewModels;

namespace IA_Ecom.Services;
public interface INotificationService
{
    void AddNotification(string message, NotificationType type);
    IEnumerable<NotificationMessage> GetNotifications();
    void ClearNotifications();
}

