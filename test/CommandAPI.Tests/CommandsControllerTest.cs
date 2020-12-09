using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.DTOs;
using CommandAPI.Models;
using CommandAPI.Data;
using Xunit;
using CommandAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CommandAPI.Profiles;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }


        /*

        Get command Tests

        */

        [Fact]
        public async Task GetCommandItems_ReturnsZeroItems_WhenDBIsEmptyAsync()
        {
            //Arrange

            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetAllCommands();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public async Task GetAllCommands_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetAllCommands();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        [Fact]
        public async Task GetCommandsByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetCommandById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCommandByID_Returns200OK_WhenValidIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).ReturnsAsync(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetCommandById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }


        /*

        Create command tests

        */

        [Fact]
        public async Task CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).ReturnsAsync(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.CreateCommand(new CommandCreateDto { });

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public async Task CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).ReturnsAsync(new Command {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = await controller.CreateCommand(new CommandCreateDto {});

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        private Task<IEnumerable<Command>> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }

            IEnumerable<Command> retCommands = commands;

            return Task.FromResult(retCommands);
        }
    }
}