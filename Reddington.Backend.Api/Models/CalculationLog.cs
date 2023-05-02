using System;
namespace Reddington.Backend.Api.Models
{
	public class CalculationLog
	{
        public DateTime Date { get; set; }
        public string FunctionName { get; set; }
        public List<double> Inputs { get; set; }
        public double Result { get; set; }
    }
}

