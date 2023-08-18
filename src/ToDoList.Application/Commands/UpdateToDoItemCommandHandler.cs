using MediatR;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Application.Entities;
using ToDoList.Application.Enums;

namespace ToDoList.Application.Commands
{
    public class UpdateToDoItemCommandHandler: IRequestHandler<UpdateToDoItemCommand, ResponseDTO>
    {
        private IToDoItemRepository _repository;

        public UpdateToDoItemCommandHandler(IToDoItemRepository repository){
            _repository = repository;
        }
        public async Task<ResponseDTO> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = new ToDoItem(request.Id, request.Title, request.Detail, request.DeadLine, (eType) request.Type, (eStatus) request.Status);
            try
            {
                if (item.IsValid())
                {
                    var toDoItem = await _repository.GetByIDAsync(request.Id);

                    if (toDoItem == null)
                    {
                        return new ResponseDTO{
                            StatusCode = eStatusCode.NotFound,
                            Message = new List<string> { "Item não encontrado" },
                            Data = null
                        };
                    }

                    toDoItem.SetTitle(request.Title);
                    toDoItem.SetDetail(request.Detail);
                    toDoItem.SetDeadLine(request.DeadLine);
                    toDoItem.SetType((eType) request.Type);
                    toDoItem.SetStatus((eStatus) request.Status);
                    
                    toDoItem.UpdatedAt = DateTime.Now;

                    _repository.Update(toDoItem);  

                    await _repository.CommitAsync();

                    return new ResponseDTO{
                        StatusCode = eStatusCode.Ok,
                        Message = new List<string>(),
                        Data = toDoItem
                    };           
                }

                return new ResponseDTO{
                    StatusCode = eStatusCode.BadRequest,
                    Message = item.Notifications.Select(e => e.Message).ToList(),
                    Data = item
                };

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                return new ResponseDTO
                {
                    StatusCode = eStatusCode.InternalServerError,
                    Message = new List<string> { "Ocorreu um erro ao executar a operação" },
                    Data = null
                };
            }
        }
    }
}