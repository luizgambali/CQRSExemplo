using ToDoList.Domain.Enums;
using ToDoList.Domain.Notifications;

namespace ToDoList.Application.Entities
{
    public class ToDoItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Detail { get; set; } = "";
        public DateTime DeadLine { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}