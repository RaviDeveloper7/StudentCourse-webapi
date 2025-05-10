using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using StudentCourseAPI;
using StudentCourseAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Get_ReturnWelcomeMessage()
        {
            var mockService = new Mock<IMessageService>();
            mockService.Setup(s => s.GetWelcomeMessage()).Returns("Hello from unit test!");

            var controller = new HomeController(mockService.Object);

            // Act
            var result = controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Hello from unit test!", result.Value);
        }

        //[Fact]
        //public void Get_ReturnsInternalServerError_WhenServiceThrowsException()
        //{
        //    // Arrange
        //    var mockService = new MessageService();

        //   // var controller = new HomeController(mockService.Object);

        //    // Act
        //    var result = mockService.GetWelcomeMessage() ;

        //    // Assert
        //    Assert.NotNull(result);
        //   // Assert.Equal(500, result.StatusCode);
        //    Assert.Equal("Something went wrong.", result.ToString());
        //}


    }
}
