using System.Collections.Generic;
using System.Threading.Tasks;

public class SqlConnection<T> : IConnection<T>
{
    public Task<IEnumerable<T>> GetItems(string gymId)
    {
        throw new System.NotImplementedException();
    }
}