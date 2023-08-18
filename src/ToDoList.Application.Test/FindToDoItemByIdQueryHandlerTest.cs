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
    public class FindToDoItemByIdQueryHandlerTest
    {
        private Mock<IToDoItemRepository> _repositoryMock = new Mock<IToDoItemRepository>();
        private FindToDoItemByIdQueryHandler _handler;        

        private IEnumerable<ToDoItem> _toDoItems; 
        public FindToDoItemByIdQueryHandlerTest()
        {
            _handler = new FindToDoItemByIdQueryHandler(_repositoryMock.Object);
            
            _toDoItems = new List<ToDoItem>(){
                                                new ToDoItem(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),"Task 1","Task 1",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted),
                                                new ToDoItem(Guid.Parse("b83df215-7453-4bf9-a15a-036f3ac5fbc8"),"Task 2","Task 2",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted),
                                                new ToDoItem(Guid.Parse("14b10ecc-72a2-45f5-85da-a97332d7b8ba"),"Task 3","Task 3",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted)
                                            };
        }

        [Fact]
        public async void FindByIdToDoItem_When_ReturnData_Returns_Success()
        {
            
            _repositoryMock.Setup(x => x.GetByIDAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_toDoItems.FirstOrDefault(x => x.Id == Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var query = new FindToDoItemByIdQuery()
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4")
            };

            var result = await _handler.Handle(query, CancellationToken.None);
            var item = _toDoItems.FirstOrDefault(x => x.Id == query.Id);

            Assert.Equal(eStatusCode.Ok, result.StatusCode);
            Assert.Equal(item, (ToDoItem) result.Data);
        }

        [Fact]
        public async void FindByIdToDoItem_When_ReturnData_Returns_Fail()
        {

            _repositoryMock.Setup(x => x.GetByIDAsync(It.IsAny<Guid>())).Returns(Task.FromResult(null as ToDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var query = new FindToDoItemByIdQuery()
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4")
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(eStatusCode.NotFound, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async void FindAllToDoItem_When_DatabaseError_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(It.IsAny<Guid>())).Throws(new Exception());

            var query = new FindToDoItemByIdQuery()
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4")
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(eStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Data);
        }
    }

}