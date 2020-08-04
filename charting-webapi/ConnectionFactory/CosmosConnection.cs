using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charting_webapi;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

public class CosmosConnection<T> : IConnection<T>
{
    private Container _container;
    private bool _seed;
    private CosmosConnection(IConfiguration configuration)
    {
        var endpoint = configuration.GetValue<string>("Cosmos:Endpoint");
        var key = configuration.GetValue<string>("Cosmos:Key");
        var databaseName = configuration.GetValue<string>("Cosmos:DatabaseName");
        var containerName = configuration.GetValue<string>("Cosmos:ContainerName");
        _seed = configuration.GetValue<bool>("Cosmos:SeedData");
        CosmosClient cosmosClient = new CosmosClient(endpoint, key);
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    private async Task<CosmosConnection<T>> InitialiseAsync()
    {
        if (_seed)
        {
            var data = this.GenerateSeedData();
            foreach (var occ in data)
            {
                await _container.CreateItemAsync<Occupancy>(occ);
            }
        }
        return this;
    }

    public static Task<CosmosConnection<T>> CreateAsync(IConfiguration configuration)
    {
        var ret = new CosmosConnection<T>(configuration);
        return ret.InitialiseAsync();
    }

    private IEnumerable<Occupancy> GenerateSeedData()
    {
        var dateTimeNow = DateTime.UtcNow;
        var gymIds = new List<string> { "000001", "000002", "000003", "000004", "000005" };
        var rand = new Random();
        var seedData = new List<Occupancy>();

        foreach (var gymId in gymIds)
        {
            seedData.AddRange(Enumerable.Range(0, 5).Select(index => new Occupancy
            {
                date = dateTimeNow.AddHours(index * -1),
                gymid = gymId,
                numberofpeople = rand.Next(0, 30),
                id = Guid.NewGuid().ToString()
            }));
        }

        return seedData;
    }

    public async Task<IEnumerable<T>> GetItems(string gymId)
    {
        var sqlQueryText = "SELECT * FROM o WHERE o.gymid='" + gymId + "'";
        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
        var occupancies = new List<T>();
        var test = _container.GetItemQueryIterator<T>(queryDefinition);

        var queryResultSetIterator = _container.GetItemQueryIterator<T>(queryDefinition);
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

