using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.Services;
using SolutionEMarket.Infrastructure.Persistence.Context;
using SolutionEMarket.Infrastructure.Persistence.Repositories;

namespace SolutionEMarket.Infrastructure.Persistence
{
    public static class InfrastructureInjectionRegistration
    {
        public static void AddServiceInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            #region Database Configuration

            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<PersistenceContext>(opt => opt.UseInMemoryDatabase("Proof"));
            }
            else
            {
                service.AddDbContext<PersistenceContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection"), 
                    mig => mig.MigrationsAssembly(typeof(PersistenceContext).Assembly.FullName)));
            }

            #endregion

            #region Injection

            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion
        }
    }
}
