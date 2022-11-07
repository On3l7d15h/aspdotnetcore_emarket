using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolutionEMarket.Core.Application.ViewModels.User;
//added
using SolutionEMarket.Core.Domain.Entities;

namespace SolutionEMarket.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LogEntity(LoginViewModel value);
        Task<bool> UsernameExist(string username);
    }
}
