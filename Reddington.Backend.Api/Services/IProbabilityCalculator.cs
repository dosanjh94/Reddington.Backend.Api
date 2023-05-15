using System;
using Reddington.Backend.Api.Models;

namespace Reddington.Backend.Api.Services
{
	public interface IProbabilityCalculator
	{
		void RegisterFunction(string functionName, IProbabilityCalculation probabilityCalculation);
        double Calculate(double probabilityA, double probabilityB, string selectedFunction);
	}
}

