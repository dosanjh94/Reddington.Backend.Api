using System;
namespace Reddington.Backend.Api.Models
{
	public class CombinedWith : IProbabilityCalculation
	{
		public CombinedWith()
		{
		}

        public double Calculate(double probabilityA, double probabilityB)
        {
            return probabilityA * probabilityB;
        }
    }
}

