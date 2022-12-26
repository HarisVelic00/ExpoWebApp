using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Repository.Repostiory
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Add(TEntity input);
        Task<TEntity> GetEntity<T>(T id);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
