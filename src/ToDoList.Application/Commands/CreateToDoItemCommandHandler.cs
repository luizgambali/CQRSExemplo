using MediatR;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Application.Entities;
using ToDoList.Application.Enums;

namespace ToDoList.Application.Commands
{
    public class CreateToDoItemCommandHandler: IRequestHandler<CreateToDoItemCommand, ResponseDTO>
    {
        private IToDoItemRepository _repository;

        public CreateToDoItemCommandHandler(IToDoItemRepository repository){
            _repository = repository;
        }
        public async Task<ResponseDTO> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = new ToDoItem(request.Title, request.Detail, request.DeadLine, (eType) request.Type, (eStatus) request.Status);
            try
            {
                if (item.IsValid())
                {
                    item.Id = Guid.NewGuid();
                    _repository.Add(item);
                    await _repository.CommitAsync();

                    return new ResponseDTO {
                        StatusCode = eStatusCode.Created,
                        Message = new List<string>(),
                        Data = item
                    };
                }
                else
                {
                    return new ResponseDTO{
                        StatusCode = eStatusCode.BadRequest,
                        Message = item.Notifications.Select(e => e.Message).ToList(),
                        Data = item
                    };
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);  

                return new ResponseDTO{
                    StatusCode = eStatusCode.InternalServerError,
                    Message = new List<string> { "Ocorreu um erro ao executar a operação" },
                    Data = null
                };
            }
            
        }
    }
}