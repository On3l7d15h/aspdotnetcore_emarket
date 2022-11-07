using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SolutionEMarket.Core.Application.Helpers;
//added
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.User;
using SolutionEMarket.Core.Domain.Entities;

namespace SolutionEMarket.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Get Methods

        public async Task<List<UserViewModel>> GetViewModel()
        {
            var listUser = await _userRepository.GetEntitiesAsync();
            return listUser
                .Select(data => new UserViewModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    LastName = data.LastName,
                    Username = data.Username,
                    Password = data.Password,
                    Phone = data.Phone,
                    Email = data.Email,
                })
                .ToList();
        }


        public async Task<SaveUserViewModel> GetSpecificSaveViewModel(int id)
        {
            var user = await _userRepository.GetSpecificEntityAsync(id);
            return new SaveUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Phone = user.Phone,
                Email = user.Email,
            };
        }

        public async Task<UserViewModel> GetLogEntity(LoginViewModel loginvm)
        {
            User user = await _userRepository.LogEntity(loginvm);

            if(user != null)
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password,
                    Phone = user.Phone,
                    Email = user.Email,
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> isUsernameExist(string username)
        {
            return await _userRepository.UsernameExist(username);
        }
        #endregion

        #region Post Methods

        public async Task<SaveUserViewModel> CreateSaveViewModel(SaveUserViewModel usermv)
        {
            User user = new User
            {
                Name = usermv.Name,
                LastName = usermv.LastName,
                Username = usermv.Username,
                Password = usermv.Password,
                Phone = usermv.Phone,
                Email = usermv.Email,
            };
            
            user = await _userRepository.CreateEntityAsync(user);

            SaveUserViewModel uservm = new()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Phone = user.Phone,
                Email = usermv.Email,
            };

            return uservm;
        }

        #endregion

        #region Update Methods

        public async Task UpdateSaveViewModel(SaveUserViewModel usermv)
        {
            User user = new User
            {
                Id = usermv.Id,
                Name = usermv.Name,
                LastName = usermv.LastName,
                Username = usermv.Username,
                Password = usermv.Password,
                Phone = usermv.Phone,
                Email = usermv.Email,
            };
            await _userRepository.UpdateEntityAsync(user);
        }

        #endregion

        #region Delete Methods

        public async Task DeleteSaveViewModel(int id)
        {
            User product = await _userRepository.GetSpecificEntityAsync(id);
            await _userRepository.DeleteEntityAsync(product);
        }

        #endregion
    }
}
