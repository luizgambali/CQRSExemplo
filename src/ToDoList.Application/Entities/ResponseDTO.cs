using ToDoList.Application.Enums;

namespace ToDoList.Application.Entities
{
    public class ResponseDTO
    {
        public eStatusCode StatusCode { get; set; }
        public List<string> Message { get; set; }
        public object Data {get; set;}
    }
}