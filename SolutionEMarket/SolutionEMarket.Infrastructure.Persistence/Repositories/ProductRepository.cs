using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Microsoft.EntityFrameworkCore;
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Core.Application.ViewModels.Product;
using SolutionEMarket.Core.Domain.Entities;
using SolutionEMarket.Infrastructure.Persistence.Context;

namespace SolutionEMarket.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly PersistenceContext _persistence;

        public ProductRepository(PersistenceContext persistenceDb) : base(persistenceDb)
        {
            _persistence = persistenceDb;
        }
        
    }
}
