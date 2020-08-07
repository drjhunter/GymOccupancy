using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using charting_models;

namespace charting_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OccupancyController : ControllerBase
    {

        private readonly ILogger<OccupancyController> _logger;
        private readonly IConnection<Occupancy> _connection;

        public OccupancyController(ILogger<OccupancyController> logger, IConnectionFactory<Occupancy> connectionFactory)
        {
            _logger = logger;
            _connection = connectionFactory.GetConnection().Result;
        }

        [HttpGet]
        public async Task<IEnumerable<Occupancy>> Get()
        {
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            return _connection.GetItems("000001").Result;

            // var occs = new List<Occupancy> {
            //     new Occupancy  { GymId = "000005", NumberOfPeople = 3},
            //     new Occupancy { GymId = "000004", NumberOfPeople = 10 },
            //     new Occupancy { GymId = "000005", NumberOfPeople = 8 }};
            // return occs.ToArray();
        }

        
    }
}
