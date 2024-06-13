using Common.Enum;

namespace Common.ViewModels
{
    public class Notification
    {
        public string Message { get; set; } = null!;
        public NotificationType Type { get; set; }
    }
}
