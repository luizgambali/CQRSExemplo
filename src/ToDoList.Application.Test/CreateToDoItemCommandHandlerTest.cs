using Moq;
using ToDoList.Application.Commands;
using ToDoList.Application.Enums;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Application.Test
{
    [Trait("Category", "CreateToDoItem")]
    public class CreateToDoItemCommandHandlerTest
    {
        private Mock<IToDoItemRepository> _repositoryMock = new Mock<IToDoItemRepository>();
        private CreateToDoItemCommandHandler _handler;

        public CreateToDoItemCommandHandlerTest()
        {
            _handler = new CreateToDoItemCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async void CreateToDoItem_When_CommandIsValid_Returns_Success()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async void CreateToDoItem_When_TitleIsNullOrEmpty_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
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
        public async void CreateToDoItem_When_DeadLineMinValue_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.MinValue,
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void CreateToDoItem_When_DetailIsNullOrEmpty_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
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
        public async void CreateToDoItem_When_DeadLineExpiredAndStatusNotStarted_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(-20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }
    
        [Fact]
        public async void CreateToDoItem_When_DeadLineExpiredAndStatusInProgress_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(-20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.InProgress
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }
    
        [Fact]
        public async void CreateToDoItem_When_DeadLineExpiredAndStatusCanceled_Returns_Fail()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(-20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.Canceled
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.BadRequest, result.StatusCode);
        }
    
        [Fact]
        public async void CreateToDoItem_When_FutureDeadLinedAndStatusCanceled_Returns_Success()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.Canceled
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async void CreateToDoItem_When_FutureDeadLinedAndStatusNotStarted_Returns_Success()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async void CreateToDoItem_When_FutureDeadLinedAndStatusCompleted_Returns_Success()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.Completed
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async void CreateToDoItem_When_ValidateResponseDTO_Returns_Created()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
                Title = "Test",
                Detail = "Test",
                DeadLine = DateTime.Now.AddDays(20),
                Type = (int) eType.Normal,
                Status = (int) eStatus.NotStarted
            };

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(eStatusCode.Created, result.StatusCode);
            Assert.Empty(result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void CreateToDoItem_When_ValidateResponseDTO_Returns_BadRequest()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>()));

            var command = new CreateToDoItemCommand
            {
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
        public async void CreateToDoItem_When_ValidateResponseDTO_Returns_InternalServerError()
        {
            _repositoryMock.Setup(x => x.Add(It.IsAny<ToDoItem>())).Throws(new Exception());

            var command = new CreateToDoItemCommand
            {
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