using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



public interface IConnectionFactory<T>
{
    Task<IConnection<T>> GetConnection<T>();
}

public class ConnectionFactory<T>: IConnectionFactory<T>
{
    private string _connectionType;
    private IConfiguration _configuration;
    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionType = configuration.GetValue<string>("ConnectionType");
        var endpoint = configuration.GetValue<string>(_connectionType + ":Endpoint");
        var key = configuration.GetValue<string>(_connectionType + ":Key");
    }

    public async Task<IConnection<T>> GetConnection<T>()
    {
        if (_connectionType == "Cosmos")
        {
            return await CosmosConnection<T>.CreateAsync(_configuration);
        }
        return null;
    }
}
