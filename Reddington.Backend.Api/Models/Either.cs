using System;
namespace Reddington.Backend.Api.Models
{
	public class Either : IProbabilityCalculation
	{
		public Either()
		{
		}

        public double Calculate(double probabilityA, double probabilityB)
        {
            return probabilityA + probabilityB - probabilityA * probabilityB;
        }
    }
}

