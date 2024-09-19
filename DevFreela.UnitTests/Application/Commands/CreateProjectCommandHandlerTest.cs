using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var projectRepository = new Mock<IProjectRepository>();

            var createProjectComand = new CreateProjectCommand
            {
                Title = "Test",
                Description = "Test",
                TotalCost = 1,
                IdClient = 1,
                IdFreelancer = 1
            };

            var craeteProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            // Act
            var id = await craeteProjectCommandHandler.Handle(createProjectComand, new CancellationToken());

            // Assert
            Assert.True(id >= 0);
        }
    }
}
