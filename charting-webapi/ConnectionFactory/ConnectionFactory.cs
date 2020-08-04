using Microsoft.Extensions.Configuration;

public interface IConnectionFactory<T>
{
    IConnection<T> GetConnection();
}

public class ConnectionFactory<T> : IConnectionFactory<T>
{
    private string _connectionType;
    public ConnectionFactory(IConfiguration configuration)
    {
        _connectionType = configuration.GetValue<string>("ConnectionType");
        var endpoint = configuration.GetValue<string>(_connectionType + ":Endpoint");
        var key = configuration.GetValue<string>(_connectionType + ":Key");
    }

    public IConnection<T> GetConnection()
    {
        if (_connectionType == "Cosmos")
        {
            return new CosmosConnection<T>();
        }
        return null;
    }
}