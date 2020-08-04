using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public interface IConnection<T>
{
    Task<IEnumerable<T>> GetItems(string gymId);
}