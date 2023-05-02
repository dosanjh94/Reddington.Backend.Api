using System;
using Newtonsoft.Json;
using Reddington.Backend.Api.Models;

namespace Reddington.Backend.Api.Services
{
	public class FileLogger : IFileLogger
    {
		public FileLogger()
		{
		}

        public void LogToFile(double probabilityA, double probabilityB, string selectedFunction, double result, string path)
        {
            string log = JsonConvert.SerializeObject(new CalculationLog
            {
                Date = DateTime.Now,
                FunctionName = selectedFunction,
                Inputs = new List<double> { probabilityA, probabilityB },
                Result = result
            });

            #if DEBUG
            File.AppendAllText(path, log + Environment.NewLine);
            #endif
        }
    }
}

