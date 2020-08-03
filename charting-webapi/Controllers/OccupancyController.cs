using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace charting_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OccupancyController : ControllerBase
    {

        private readonly ILogger<OccupancyController> _logger;

        public OccupancyController(ILogger<OccupancyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Occupancy> Get()
        {
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            var occs = new List<Occupancy> {
                new Occupancy  { GymId = "000005", NumberOfPeople = 3},
                new Occupancy { GymId = "000004", NumberOfPeople = 10 },
                new Occupancy { GymId = "000005", NumberOfPeople = 8 }};
            return occs.ToArray();
        }
    }
}
