using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<List<Entity>> GetEntitiesAsync();
        Task<Entity> GetSpecificEntityAsync(int id);
        Task<Entity> CreateEntityAsync(Entity value);
        Task UpdateEntityAsync(Entity value);
        Task DeleteEntityAsync(Entity value);
    }
}
