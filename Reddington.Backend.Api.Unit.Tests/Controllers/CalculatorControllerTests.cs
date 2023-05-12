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
        private readonly Mock<IProbabilityCalculator> _mockProbabilityCalculator;
        private readonly Mock<IFileLogger> _mockFileLogger;
        private CalculatorController _sut;

        public CalculatorControllerTests()
        {
            _mockFileLogger = new Mock<IFileLogger>();
            _mockProbabilityCalculator = new Mock<IProbabilityCalculator>();
            _sut = new CalculatorController(_mockProbabilityCalculator.Object, _mockFileLogger.Object);
        }

        [Theory]
        [InlineData(0.5, 0.5, "combinedwith", 0.25)]
        [InlineData(0.5, 0.5, "either", 0.75)]
        [InlineData(0, 0, "combinedwith", 0)]
        [InlineData(0, 0, "either", 0)]
        [InlineData(1, 1, "combinedwith", 1)]
        [InlineData(1, 1, "either", 1)]
        public void Get_ReturnsOk_WhenInputsAreValid(double probabilityA, double probabilityB, string selectedFunction, double expectedResult)
        {
            // Act
            var result = _sut.Get(probabilityA, probabilityB, selectedFunction);

            // Assert
            var okResult = Assert.IsType<ActionResult<CalculationResult>>(result);
            var calculationResult = Assert.IsType<CalculationResult>(okResult.Value);
            Assert.Equal(expectedResult, calculationResult.Result);

            _mockProbabilityCalculator.Verify(x => x.Calculate(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
            _mockFileLogger.Verify(x => x.LogToFile(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData(-0.1, 0.5, "combinedwith")]
        [InlineData(1.1, 0.5, "either")]
        [InlineData(0.5, -0.1, "combinedwith")]
        [InlineData(0.5, 1.1, "either")]
        public void Get_ReturnsBadRequest_WhenProbabilityIsInvalid(double probabilityA, double probabilityB, string selectedFunction)
        {
            // Act
            var result = _sut.Get(probabilityA, probabilityB, selectedFunction);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            _mockProbabilityCalculator.Verify(x => x.Calculate(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
            _mockFileLogger.Verify(x => x.LogToFile(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
        }
    }
}

