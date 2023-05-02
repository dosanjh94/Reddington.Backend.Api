using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reddington.Backend.Api.Models;
using Reddington.Backend.Api.Services;

namespace Reddington.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly string _logFilePath = "log.txt";
    private readonly IFileLogger _fileLogger;
    public CalculatorController(IFileLogger filelogger)
    {
        _fileLogger = filelogger;
    }

    [HttpGet(Name = "GetProbability")]
    public ActionResult<CalculationResult> Get([FromQuery] double probabilityA, [FromQuery] double probabilityB, [FromQuery] string selectedFunction)
    {
        if (probabilityA < 0 || probabilityA > 1 || probabilityB < 0 || probabilityB > 1)
        {
            return BadRequest("Invalid probability range (must be between 0 and 1)");
        }

        // perform calculation
        double result = selectedFunction.ToLower() switch
        {
            "combinedwith" => probabilityA * probabilityB,
            "either" => probabilityA + probabilityB - probabilityA * probabilityB,
            _ => throw new ArgumentException("Invalid function name")
        };

        _fileLogger.LogToFile(probabilityA, probabilityB, selectedFunction, result, _logFilePath);

        return new CalculationResult { Result = result };
    }
}

