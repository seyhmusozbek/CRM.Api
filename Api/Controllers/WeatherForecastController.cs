using Business;
using Core.Core.CrossCutting.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IClass1 _denemeClass;

    public WeatherForecastController(
        IClass1 denemeClass,
        IServiceProvider serviceProvider,
        ILogger<WeatherForecastController> logger
        )
    {
        _denemeClass = TransparentProxy<IClass1>.GenerateProxy(denemeClass,serviceProvider);
        _logger = logger;
    }
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;


    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _denemeClass.Denem();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}