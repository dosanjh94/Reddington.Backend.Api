using System;
namespace Reddington.Backend.Api.Models
{
	public class CalculationRequest
	{
        public double ProbabilityA { get; set; }
        public double ProbabilityB { get; set; }
        public string SelectedFunction { get; set; }
    }
}

