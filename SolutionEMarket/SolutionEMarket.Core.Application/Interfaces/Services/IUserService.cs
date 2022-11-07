using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using SolutionEMarket.Core.Application.ViewModels.User;
using SolutionEMarket.Core.Domain.Entities;

namespace SolutionEMarket.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> GetLogEntity(LoginViewModel loginvm);
        Task<bool> isUsernameExist(string username);
    }
}
