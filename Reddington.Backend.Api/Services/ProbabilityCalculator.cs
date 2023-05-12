using System;
namespace Reddington.Backend.Api.Services
{
	public class ProbabilityCalculator : IProbabilityCalculator
	{
        public double Calculate(double probabilityA, double probabilityB, string selectedFunction)
        {
            return selectedFunction.ToLower() switch
            {
                "combinedwith" => probabilityA * probabilityB,
                "either" => probabilityA + probabilityB - probabilityA * probabilityB,
                _ => throw new ArgumentException("Invalid function name")
            };
        }
    }
}

