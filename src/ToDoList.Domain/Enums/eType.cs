using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.Enums
{
    public enum eType 
    {
        Normal = 0,
        Important = 1,
        Urgent = 2
    }
    public class TypeValidation
    {
        public static bool Validate(int value)
        {
            var values = Enum.GetValues(typeof(eType)).Cast<int>().ToList();
            return values.Any(x => x == value);
        }
    }
}