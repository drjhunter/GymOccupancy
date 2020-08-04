using System.Collections.Generic;
using System.Threading.Tasks;

public interface IConnection<T>
{
    public Task<IEnumerable<T>> GetItems();
}