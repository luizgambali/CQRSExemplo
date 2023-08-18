using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Util;
using ToDoList.Application.Commands;
using ToDoList.Application.Entities;
using ToDoList.Application.Queries;
using ToDoList.Application.QueryHandlers;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Infrastructure.Context;
using ToDoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IRequestHandler<CreateToDoItemCommand, ResponseDTO>, CreateToDoItemCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateToDoItemCommand, ResponseDTO>, UpdateToDoItemCommandHandler>();
builder.Services.AddScoped<IRequestHandler<FindToDoItemByIdQuery, ResponseDTO>, FindToDoItemByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<FindAllToDoItemQuery, ResponseDTO>, FindAllToDoItemQueryHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

DatabaseUtil.Create();

app.MapGet("api/todolist/{id}", async ([FromServices] IMediator mediator, Guid id) => {

    var command = new  FindToDoItemByIdQuery() { Id = id };

    var result = await mediator.Send(command);

    return CustomResult.CustomResponse(result);
});

app.MapGet("api/todolist", async ([FromServices] IMediator mediator) => {

    var command = new  FindAllToDoItemQuery() { };

    var result = await mediator.Send(command);

    return CustomResult.CustomResponse(result);
});

app.MapPost("api/todolist", async ([FromServices] IMediator mediator, [FromBody] CreateToDoItemCommand command) => {
    
    var result = await mediator.Send(command);

    return CustomResult.CustomResponse(result);
});

app.MapPut("api/todolist", async ([FromServices] IMediator mediator, [FromBody] UpdateToDoItemCommand command) => {
    
    var result = await mediator.Send(command);

    return CustomResult.CustomResponse(result);
});

app.Run();
