using Moq;
using ToDoList.Application.Commands;
using ToDoList.Application.Enums;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Application.Test
{
    [Trait("Category", "UpdateToDoItem")]
    public class UpdateToDoItemCommandHandlerTest
    {
        private Mock<IToDoItemRepository> _repositoryMock = new Mock<IToDoItemRepository>();
        private UpdateToDoItemCommandHandler _handler;
        private ToDoItem _toDoItem; 

        public UpdateToDoItemCommandHandlerTest()
        {
            _toDoItem = new ToDoItem(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),"Test123","Test123",DateTime.Now.AddDays(20),eType.Normal,eStatus.NotStarted);
            _handler = new UpdateToDoItemCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async void UpdateToDoItem_When_CommandIsValid_Returns_Success()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_InvalidStatus_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = 55
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_InvalidType_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = 55,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_TitleIsNullOrEmpty_Returns_Fail()
        {           
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).ReturnsAsync(_toDoItem);
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_DetailIsNullOrEmpty_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test",
                Detail = "",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_DeadLineMinValue_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).ReturnsAsync(_toDoItem);
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.MinValue,
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_DeadLineExpiredAndStatusNotStarted_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.Now.AddDays(-20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }
    
        [Fact]
        public async void UpdateToDoItem_When_DeadLineExpiredAndStatusInProgress_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.Now.AddDays(-20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.InProgress
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }
    
        [Fact]
        public async void UpdateToDoItem_When_DeadLineExpiredAndStatusCanceled_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.Now.AddDays(-20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.Canceled
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_NotFoundToDoItem_Returns_NotFound()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb34444"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.NotFound, result.StatusCode);
        }
    
        [Fact]
        public async void UpdateToDoItem_When_FutureDeadLinedAndStatusCanceled_Returns_Success()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test123",
                Detail = "Test123",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.Canceled
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Ok, result.StatusCode);            
        }

        [Fact]
        public async void UpdateToDoItem_When_FutureDeadLinedAndStatusNotStarted_Returns_Success()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_FutureDeadLinedAndStatusCompleted_Returns_Success()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.Completed
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async void UpdateToDoItem_When_ValidateResponseDTO_Returns_Ok()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Ok, result.StatusCode);
            Assert.Empty(result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void UpdateToDoItem_When_ValidateResponseDTO_Returns_BadRequest()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>()));

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void UpdateToDoItem_When_ValidateResponseDTO_Returns_InternalServerError()
        {
            _repositoryMock.Setup(x => x.GetByIDAsync(Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"))).Returns(Task.FromResult(_toDoItem));
            _repositoryMock.Setup(x => x.Update(It.IsAny<ToDoItem>())).Throws(new Exception());

            var command = new UpdateToDoItemCommand
            {
                Id = Guid.Parse("2880e168-d50a-4a77-82f4-06fd8bb3e4f4"),
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.InternalServerError, result.StatusCode);
            Assert.NotEmpty(result.Message);
            Assert.Null(result.Data);
        }
    }
}