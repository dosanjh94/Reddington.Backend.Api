using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Reddington.Backend.Api.Controllers;
using Reddington.Backend.Api.Models;
using Reddington.Backend.Api.Services;

namespace Reddington.Backend.Api.Unit.Tests.Controllers
{
	public class CalculatorControllerTests
	{
        [Theory]
        [InlineData(0.5, 0.5, "combinedwith", 0.25)]
        [InlineData(0.5, 0.5, "either", 0.75)]
        [InlineData(0, 0, "combinedwith", 0)]
        [InlineData(0, 0, "either", 0)]
        [InlineData(1, 1, "combinedwith", 1)]
        [InlineData(1, 1, "either", 1)]
        public void Get_Returns_CalculationResult(double probabilityA, double probabilityB, string selectedFunction, double expectedResult)
        {
            // Arrange
            var mockFileLogger = new Mock<IFileLogger>();
            var controller = new CalculatorController(mockFileLogger.Object);

            // Act
            var result = controller.Get(probabilityA, probabilityB, selectedFunction);

            // Assert
            var okResult = Assert.IsType<ActionResult<CalculationResult>>(result);
            var calculationResult = Assert.IsType<CalculationResult>(okResult.Value);
            Assert.Equal(expectedResult, calculationResult.Result);

            //Low on time would normally verify explicitly
            mockFileLogger.Verify(x => x.LogToFile(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData(-0.1, 0.5, "combinedwith")]
        [InlineData(1.1, 0.5, "either")]
        [InlineData(0.5, -0.1, "combinedwith")]
        [InlineData(0.5, 1.1, "either")]
        public void Get_Returns_BadRequest_When_InputOutOfRange(double probabilityA, double probabilityB, string selectedFunction)
        {
            // Arrange
            var mockFileLogger = new Mock<IFileLogger>();
            var controller = new CalculatorController(mockFileLogger.Object);

            // Act
            var result = controller.Get(probabilityA, probabilityB, selectedFunction);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            mockFileLogger.Verify(x => x.LogToFile(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Get_Throws_ArgumentException_When_InvalidFunctionName()
        {
            // Arrange
            var mockFileLogger = new Mock<IFileLogger>();
            var controller = new CalculatorController(mockFileLogger.Object);
            var probabilityA = 0.5;
            var probabilityB = 0.5;
            var selectedFunction = "InvalidName";

            Assert.Throws<ArgumentException>(() => controller.Get(probabilityA, probabilityB, selectedFunction));

        }
        }
}

