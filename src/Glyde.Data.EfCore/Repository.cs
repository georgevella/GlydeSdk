using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Glyde.Data.EfCore
{
    public class Repository : IRepository
    {
        private readonly DbContext _dbContext;

        public Repository()
        {

        }

        public async Task<T> Get<T>(object id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Add<T>(T item) where T : class
        {
            var result = await _dbContext.Set<T>().AddAsync(item);
            return result.Entity;
        }

        public async Task Delete<T>(T item) where T : class
        {
            await Task.Run(() => _dbContext.Set<T>().Remove(item));
        }

        public async Task Update<T>(T item) where T : class
        {
            await Task.Run(() => _dbContext.Update(item));
        }
    }
}