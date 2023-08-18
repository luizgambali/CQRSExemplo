using MediatR;
using ToDoList.Application.Entities;

namespace ToDoList.Application.Commands
{
    public class UpdateToDoItemCommand: IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Detail { get; set; } = "";
        public DateTime DeadLine { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
    }
}