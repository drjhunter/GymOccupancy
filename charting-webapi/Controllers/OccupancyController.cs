using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;

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
        public async Task<IEnumerable<Occupancy>> Get()
        {
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            return await GetFromStorage();

            // var occs = new List<Occupancy> {
            //     new Occupancy  { GymId = "000005", NumberOfPeople = 3},
            //     new Occupancy { GymId = "000004", NumberOfPeople = 10 },
            //     new Occupancy { GymId = "000005", NumberOfPeople = 8 }};
            // return occs.ToArray();
        }

        private async Task<IEnumerable<Occupancy>> GetFromStorage()
        {
            // Initialize a new instance of CosmosClient using the built-in account endpoint and key parameters
            var endpoint = "https://charting-stor.documents.azure.com:443/";
            var key = "6rcotecXI2m7VpruVbL0F9di9kcpa169LLHZ27sycWkAJuPBsHwnndIOzYA6PbMPeYoWUCi7alGLopcmgVxMuQ==";
            var databaseName = "gymoccupancydb";
            var containerName = "hourlyoccupancy";

            var sqlQueryText = "SELECT * FROM o WHERE o.gymid='000001'";
            CosmosClient cosmosClient = new CosmosClient(endpoint, key);
            var container = cosmosClient.GetContainer(databaseName, containerName);
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            var occupancies = new List<Occupancy>();
            var test = container.GetItemQueryIterator<Occupancy>(queryDefinition);

            var queryResultSetIterator = container.GetItemQueryIterator<Occupancy>(queryDefinition);
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Occupancy> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Occupancy occ in currentResultSet)
                {
                    occupancies.Add(occ);
                }
            }
            return occupancies;
        }
    }
}
