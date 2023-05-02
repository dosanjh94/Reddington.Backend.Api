using System;
namespace Reddington.Backend.Api.Services
{
	public interface IFileLogger
	{
		public void LogToFile(double probabilityA, double probabilityB, string selectedFunction, double result, string path);
	}
}

