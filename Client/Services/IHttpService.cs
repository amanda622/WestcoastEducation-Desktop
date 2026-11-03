using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services;

public interface IHttpService<T> where T : class
{
    Task<List<T>> GetValuesAsync(string resource);

}
