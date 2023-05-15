using System;
using Reddington.Backend.Api.Models;

namespace Reddington.Backend.Api.Services
{
	public class ProbabilityCalculator : IProbabilityCalculator
	{
        private readonly Dictionary<string, IProbabilityCalculation> _functions;
        public ProbabilityCalculator()
        {
            _functions = new Dictionary<string, IProbabilityCalculation>();
        }
        public void RegisterFunction(string functionName, IProbabilityCalculation probabilityCalculation)
        {
            _functions[functionName] = probabilityCalculation;
        }
        public double Calculate(double probabilityA, double probabilityB, string selectedFunction)
        {
            try
            {
                return _functions[selectedFunction.ToLower()].Calculate(probabilityA, probabilityB);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Invalid function name");
            }
        }
    }
}

