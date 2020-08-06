using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using charting_models;

namespace GymOccupancy.Function
{
    public static class OccupancyDataProcessorFn
    {
        [FunctionName("OccupancyDataProcessorFn")]
        public static void Run(
            [BlobTrigger("rawoccupancydata/{name}", Connection = "gymoccupancystor_STORAGE")] Stream myBlob, 
            string name, 
            [CosmosDB("gymoccupancydb", "hourlyoccupancy", ConnectionStringSetting = "CosmosDBConnection")] out dynamic cosmosDocument, 
            ILogger log)
        {
            if (name.EndsWith(".json"))
            {
                StreamReader reader = new StreamReader(myBlob);
                string responseAsString = reader.ReadToEnd();
                cosmosDocument = JsonConvert.DeserializeObject<Occupancy>(responseAsString);
            }
            cosmosDocument = new object();
        }
    }
}
