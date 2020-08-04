using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

public class CosmosConnection<T> : IConnection<T>
{
    public CosmosConnection()
    {

    }

    public async Task<IEnumerable<T>> GetItems()
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
            var occupancies = new List<T>();
            var test = container.GetItemQueryIterator<T>(queryDefinition);

            var queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T occ in currentResultSet)
                {
                    occupancies.Add(occ);
                }
            }
            return occupancies;
        }
}

