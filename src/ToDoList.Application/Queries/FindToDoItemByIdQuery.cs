using MediatR;
using ToDoList.Application.Entities;

namespace ToDoList.Application.Queries
{
    public class FindToDoItemByIdQuery: IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}