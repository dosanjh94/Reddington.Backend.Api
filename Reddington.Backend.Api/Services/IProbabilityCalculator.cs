using System;
namespace Reddington.Backend.Api.Services
{
	public interface IProbabilityCalculator
	{
		double Calculate(double probabilityA, double probabilityB, string selectedFunction);
	}
}

