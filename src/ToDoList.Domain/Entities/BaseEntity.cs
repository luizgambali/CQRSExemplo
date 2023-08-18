using ToDoList.Domain.Notifications;

namespace ToDoList.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        private List<Notification> _notifications = new List<Notification>();
        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }
        public void ClearNotifications()
        {
            _notifications?.Clear();
        }
    }

}