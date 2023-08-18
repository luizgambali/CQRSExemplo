using ToDoList.Domain.Enums;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Notifications;

namespace ToDoList.Domain.Entities
{
    public class ToDoItem : BaseEntity, IValidation
    {
        public string Title { get; private set; } = "";
        public string Detail { get; private set; } = "";
        public DateTime DeadLine { get; private set; }
        public eType Type { get; private set; }
        public eStatus Status { get; private set; }

        public ToDoItem() {}
        
        public ToDoItem(Guid id, string title, string detail, DateTime deadLine, eType type, eStatus status)
        {
            Id = id;
            Title = title;
            Detail = detail;
            DeadLine = deadLine;
            Type = type;
            Status = status;

            Validate();
        }

        public ToDoItem(string title, string detail, DateTime deadLine, eType type, eStatus status)
        {
            Id = Guid.NewGuid();
            Title = title;
            Detail = detail;
            DeadLine = deadLine;
            Type = type;
            Status = status;

            Validate();
        }

        public void SetTitle(string title)
        {
            Title = title;

            Validate();
        }

        public void SetDetail(string detail)
        {
            Detail = detail;

            Validate();
        }

        public void SetDeadLine(DateTime deadLine)
        {
            DeadLine = deadLine;

            Validate();
        }
        public void SetType(eType type)
        {
            Type = type;

            Validate();
        }

        public void SetStatus(eStatus status)
        {
            Status = status;

            Validate();
        }
        
        private void Validate()
        {
            ClearNotifications();

            if (string.IsNullOrEmpty(Title)) 
                AddNotification(new Notification("Title", "Título não pode ser vazio"));

            if (string.IsNullOrEmpty(Detail)) 
                AddNotification(new Notification("Detail", "Detalhe não pode ser vazio"));

            if (DeadLine == DateTime.MinValue) 
                AddNotification(new Notification("DeadLine", "Data de vencimento não pode ser vazia"));

            if (DeadLine <= DateTime.Now && Status != eStatus.Completed) 
                AddNotification(new Notification("DeadLine", "Tarefas com data de vencimento anteriore a data atual devem estar com o status de concluída"));            
        }

        public bool IsValid()
        {
            return Notifications.Count == 0;
        }

        public override bool Equals(object obj)
        {
            var objToDoItem = obj as ToDoItem;

            return objToDoItem.Id == Id &&
                   objToDoItem.Title == Title &&
                   objToDoItem.Detail == Detail &&
                   objToDoItem.DeadLine == DeadLine &&
                   objToDoItem.Type == Type &&
                   objToDoItem.Status == Status;
        }
    }
}