using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glyde.Data
{
    public interface IRepository
    {
        Task<T> Get<T>(object id)
            where T: class;

        Task<T> Add<T>(T item) where T : class;

        Task Delete<T>(T item) where T : class;

        Task Update<T>(T item) where T : class;
    }
}