using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GymOccupancy.Function
{
    public static class OccupancyDataProcessorFn
    {
        [FunctionName("OccupancyDataProcessorFn")]
        public static void Run([BlobTrigger("rawoccupancydata/{name}", Connection = "gymoccupancystor_STORAGE")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
