public class SqlConnection<T> : IConnection<T>
{
    public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<T>> GetItems()
    {
        throw new System.NotImplementedException();
    }
}