using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application
{
    public static class ServiceRegistration
    {

        public static void AddApplicationLayer(this IServiceCollection service, IConfiguration config)
        {
            #region services

            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IUserService, UserService>();

            #endregion
        }

    }
}
