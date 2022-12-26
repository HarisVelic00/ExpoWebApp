using ExpoApp.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Repository.Repostiory
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ExpoContext ExpoContext;
        private readonly DbSet<TEntity> Entities;

        public Repository(ExpoContext expoContext)
        {
            ExpoContext = expoContext;
            Entities = ExpoContext.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity input)
        {
            await Entities.AddAsync(input);
            await ExpoContext.SaveChangesAsync();

            return input;
        }

        public async Task Delete(TEntity entity)
        {
            Entities.Remove(entity);
            await ExpoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var data = Entities.AsEnumerable();

            return data;
        }

        public async Task<TEntity> GetEntity<T>(T id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            ExpoContext.Entry(entity).State = EntityState.Modified;
            await ExpoContext.SaveChangesAsync();

            return entity;
        }
    }
}
