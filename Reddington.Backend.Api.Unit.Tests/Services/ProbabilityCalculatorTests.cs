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
            _sut.RegisterFunction("either", new Either());
            _sut.RegisterFunction("combinedwith", new CombinedWith());
        }

        [Theory]
        [InlineData(0.5, 0.5, "combinedwith", 0.25)]
        [InlineData(0.5, 0.5, "combinedwith", 0.24)]
        [InlineData(0.3, 0.7, "either", 0.79)]
        [InlineData(0.2, 0.8, "either", 0.84)]

        public void Calculate_ValidInput_ReturnsExpectedResult(double probabilityA, double probabilityB, string selectedFunction, double expectedResult)
        {
            double actualResult = _sut.Calculate(probabilityA, probabilityB, selectedFunction);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Calculate_InvalidFunctionName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _sut.Calculate(0.5, 0.5, "invalid"));
        }
    }
}

