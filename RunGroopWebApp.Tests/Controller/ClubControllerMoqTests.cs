using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RunGroopWebApp.Tests.Controller
{
    public class ClubControllerMoqTests
    {
        private readonly Mock<IClubRepository> _clubRepositoryMock;
        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly ClubController _clubController;
        public ClubControllerMoqTests()
        {
            _clubRepositoryMock = new Mock<IClubRepository>();
            _photoServiceMock = new Mock<IPhotoService>();
            _clubController = new ClubController(_clubRepositoryMock.Object, _photoServiceMock.Object);
        }
       

        [Fact]
        public async Task ClubController_Detail_ReturnSuccess()
        {

            //Arrange
            var id = 5;
            var clubMock = new Club();
            _clubRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(clubMock);
            //Act
            var result = await _clubController.DetailClub(id);

            //Assert
            result.Should().BeOfType<ViewResult>();


        }


    }
}
