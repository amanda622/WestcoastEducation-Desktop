using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services;

public interface IHttpService<T> where T : class
{
    Task<List<T>> GetValuesAsync(string resource);

    Task PostAsync(string resource, T item);

    Task PutAsync(string resource, string id, T item);

    Task DeleteAsync(string resource, string id);
}
