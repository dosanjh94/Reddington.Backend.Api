using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using Reddington.Backend.Api.Models;
using Reddington.Backend.Api.Services;

namespace Reddington.Backend.Api.Unit.Tests.Services
{
	public class ProbabilityCalculatorTests
	{
		private ProbabilityCalculator _sut;
		public ProbabilityCalculatorTests()
		{
            _sut = new ProbabilityCalculator();
		}

        [Theory]
        [InlineData(0.5, 0.5, "combinedwith", 0.25)]
        [InlineData(0.5, 0.5, "combinedwith", 0.24)]
        [InlineData(0.3, 0.7, "either", 0.79)]
        [InlineData(0.2, 0.8, "either", 0.84)]

        public void Calculate_ValidInput_ReturnsExpectedResult(double probabilityA, double probabilityB, string selectedFunction, double expectedResult)
        {
            // Arrange
            var calculator = new ProbabilityCalculator();

            // Act
            double actualResult = calculator.Calculate(probabilityA, probabilityB, selectedFunction);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Calculate_InvalidFunctionName_ThrowsArgumentException()
        {
            // Arrange
            var calculator = new ProbabilityCalculator();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.Calculate(0.5, 0.5, "invalid"));
        }
    }
}

