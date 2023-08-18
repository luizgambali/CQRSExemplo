using Moq;
using ToDoList.Application.Enums;
using ToDoList.Application.Queries;
using ToDoList.Application.QueryHandlers;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Application.Test
{
    [Trait("Category", "UpdateToDoItem")]
    public class FindAllToDoItemQueryHandlerTest
    {
        private Mock<IToDoItemRepository> _repositoryMock = new Mock<IToDoItemRepository>();
        private FindAllToDoItemQueryHandler _handler;        

        private IEnumerable<ToDoItem> _toDoItems; 
        public FindAllToDoItemQueryHandlerTest()
        {
            _handler = new FindAllToDoItemQueryHandler(_repositoryMock.Object);
            
            _toDoItems = new List<ToDoItem>(){
                                                new ToDoItem(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),"Task 1","Task 1",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted),
                                                new ToDoItem(Guid.Parse("b83df215-7453-4bf9-a15a-036f3ac5fbc8"),"Task 2","Task 2",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted),
                                                new ToDoItem(Guid.Parse("14b10ecc-72a2-45f5-85da-a97332d7b8ba"),"Task 3","Task 3",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted)
                                            };
        }

        [Fact]
        public async void FindAllToDoItem_When_ReturnData_Returns_Success()
        {
            _repositoryMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(_toDoItems));

            var result = await _handler.Handle(new FindAllToDoItemQuery(), CancellationToken.None);

            Assert.Equal(eStatusCode.Ok, result.StatusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void FindAllToDoItem_When_NoData_Returns_Empty()
        {
            _toDoItems = new List<ToDoItem>();

            _repositoryMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(_toDoItems));

            var result = await _handler.Handle(new FindAllToDoItemQuery(), CancellationToken.None);

            Assert.Equal(eStatusCode.Ok, result.StatusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void FindAllToDoItem_When_DatabaseError_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetAllAsync()).Throws(new Exception());

            var result = await _handler.Handle(new FindAllToDoItemQuery(), CancellationToken.None);

            Assert.Equal(eStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Data);
        }
    }

}