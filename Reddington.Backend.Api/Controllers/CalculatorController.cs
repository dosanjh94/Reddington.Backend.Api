using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reddington.Backend.Api.Models;
using Reddington.Backend.Api.Services;

namespace Reddington.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly IProbabilityCalculator _probabilityCalculator;
    private readonly IFileLogger _fileLogger;
    private readonly string _logFilePath = "log.txt";
    public CalculatorController(IProbabilityCalculator probabilityCalculator, IFileLogger filelogger)
    {
        _probabilityCalculator = probabilityCalculator;
        _fileLogger = filelogger;
    }

    [HttpGet(Name = "GetProbability")]
    public ActionResult<CalculationResult> Get([FromQuery] double probabilityA, [FromQuery] double probabilityB, [FromQuery] string selectedFunction)
    {
        if (probabilityA < 0 || probabilityA > 1 || probabilityB < 0 || probabilityB > 1)
        {
            return BadRequest("Invalid probability range (must be between 0 and 1)");
        }

        RegisterFunctions();
        var result = _probabilityCalculator.Calculate(probabilityA, probabilityB, selectedFunction);

        _fileLogger.LogToFile(probabilityA, probabilityB, selectedFunction, result, _logFilePath);

        return new CalculationResult { Result = result };
    }

    private void RegisterFunctions()
    {
        _probabilityCalculator.RegisterFunction("either", new Either());
        _probabilityCalculator.RegisterFunction("combinedwith", new CombinedWith());
    }
}

