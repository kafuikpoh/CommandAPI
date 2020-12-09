using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something",
                Platform = "Some platform",
                CommandLine = "Some commandLine"
            };
        }

        public void Dispose()
        {
            testCommand = null;
        }


        [Fact]
        public void CanChangeHowTo()
        {
            //Arrange


            //Act
            testCommand.HowTo = "Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            //Arrange


            //Act
            testCommand.Platform = "gUnit";

            //Assert
            Assert.Equal("gUnit", testCommand.Platform);
        }
    }
}