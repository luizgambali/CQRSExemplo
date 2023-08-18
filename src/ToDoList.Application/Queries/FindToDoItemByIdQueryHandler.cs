using MediatR;
using ToDoList.Application.Entities;
using ToDoList.Application.Enums;
using ToDoList.Application.Queries;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Application.QueryHandlers
{
    public class FindToDoItemByIdQueryHandler: IRequestHandler<FindToDoItemByIdQuery, ResponseDTO>
    {
        private IToDoItemRepository _repository;

        public FindToDoItemByIdQueryHandler(IToDoItemRepository repository){
            _repository = repository;
        }
        public async Task<ResponseDTO> Handle(FindToDoItemByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _repository.GetByIDAsync(request.Id);

                if (item == null) 
                {
                    return new ResponseDTO
                    {
                        StatusCode = eStatusCode.NotFound,
                        Message = new List<string>(){ "Item não encontrado" },
                        Data = null
                    };
                }

                return new ResponseDTO {
                    StatusCode = eStatusCode.Ok,
                    Message = new List<string>(),
                    Data = item
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