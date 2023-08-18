namespace ToDoList.Domain.Notifications
{
    public class Notification 
    {
        public string PropertyName { get; private set; }
        public string Message { get; private set; }

        public Notification(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }
}