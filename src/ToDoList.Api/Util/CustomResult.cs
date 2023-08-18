using ToDoList.Application.Entities;
using ToDoList.Application.Enums;

namespace ToDoList.Api.Util
{
    public static class CustomResult
    {
        public static IResult CustomResponse(ResponseDTO responseDTO)
        {
            switch (responseDTO.StatusCode)
            {
                case eStatusCode.Ok:
                    return Results.Ok(responseDTO.Data);
                case eStatusCode.Created:
                    return Results.Created($"/api/todolist/{responseDTO.Data}", responseDTO.Data);
                case eStatusCode.NotFound:
                    return Results.NotFound(responseDTO.Message);
                case eStatusCode.InternalServerError:
                    return Results.Problem(responseDTO.Message[0]);
                default:
                    return Results.BadRequest(responseDTO.Message);
            }
        }
    }

}