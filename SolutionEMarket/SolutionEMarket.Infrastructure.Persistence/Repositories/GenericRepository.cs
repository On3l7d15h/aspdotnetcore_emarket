using Microsoft.EntityFrameworkCore;
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly PersistenceContext _persistence;
        public GenericRepository(PersistenceContext persistenceDb)
        {
            _persistence = persistenceDb;
        }

        #region GetMethods

        public virtual async Task<List<Entity>> GetEntitiesAsync()
        {
            return await _persistence.Set<Entity>().ToListAsync();
        }

        public virtual async Task<Entity> GetSpecificEntityAsync(int id)
        {
            return await _persistence.Set<Entity>().FindAsync(id);
        }

        #endregion

        #region Post Methods

        public virtual async Task<Entity> CreateEntityAsync(Entity value)
        {
            await _persistence.Set<Entity>().AddAsync(value);
            await _persistence.SaveChangesAsync();

            return value;
        }

        #endregion 

        #region Update Methods

        public virtual async Task UpdateEntityAsync(Entity value)
        {
            _persistence.Entry(value).State = EntityState.Modified;
            await _persistence.SaveChangesAsync();
        }

        #endregion 

        #region Delete Methods

        public virtual async Task DeleteEntityAsync(Entity value)
        {
            _persistence.Set<Entity>().Remove(value);
            await _persistence.SaveChangesAsync();
        }

        #endregion 
    }
}
