using MediatR;
using ToDoList.Application.Entities;
using ToDoList.Application.Enums;
using ToDoList.Application.Queries;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Application.QueryHandlers
{
    public class FindAllToDoItemQueryHandler: IRequestHandler<FindAllToDoItemQuery, ResponseDTO>
    {
        private IToDoItemRepository _repository;

        public FindAllToDoItemQueryHandler(IToDoItemRepository repository){
            _repository = repository;
        }

        public async Task<ResponseDTO> Handle(FindAllToDoItemQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _repository.GetAllAsync();

                return new ResponseDTO {
                    StatusCode = eStatusCode.Ok,
                    Message = new List<string>(),
                    Data = items
                };
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