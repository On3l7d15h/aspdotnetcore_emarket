using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using SolutionEMarket.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SolutionEMarket.Core.Domain.Entities;
using SolutionEMarket.Core.Application.Interfaces.Repositories;

namespace SolutionEMarket.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly PersistenceContext _persistence;

        public CategoryRepository(PersistenceContext persistence) : base(persistence)
        {
            _persistence = persistence;
        }

        
    }
}
