namespace IA_Ecom.ViewModels;

public class NotificationMessage
{
    public string Message { get; set; }
    public NotificationType Type { get; set; }
}
public enum NotificationType
{
    Success,
    Error,
    Validation
}
