using MediatR;
using ToDoList.Application.Entities;

namespace ToDoList.Application.Queries
{
    public class FindAllToDoItemQuery: IRequest<ResponseDTO>
    {
    }
}