using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.Enums
{
    public enum eStatus 
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Canceled = 3
    }

    public class StatusValidation
    {
        public static bool Validate(int value)
        {
            var values = Enum.GetValues(typeof(eStatus)).Cast<int>().ToList();
            return values.Any(x => x == value);
        }
    }
}