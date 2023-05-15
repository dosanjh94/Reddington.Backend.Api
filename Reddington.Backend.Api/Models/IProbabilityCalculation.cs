using System;
namespace Reddington.Backend.Api.Models
{
	public interface IProbabilityCalculation
	{
		public double Calculate(double probablilityA, double probabilityB);
	}
}

