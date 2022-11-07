using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolutionEMarket.Core.Application.Helpers;
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Core.Application.ViewModels.User;
using SolutionEMarket.Core.Domain.Entities;
//added
using SolutionEMarket.Infrastructure.Persistence.Context;

namespace SolutionEMarket.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly PersistenceContext _persistence;

        public UserRepository(PersistenceContext persistence) : base(persistence)
        {
            _persistence = persistence;
        }

        //override methods
        public override async Task<User> CreateEntityAsync(User value)
        {
            value.Password = PasswordEncryptation.PasswordHash(value.Password);

            if (await this.UsernameExist(value.Username))
            {
                return new User();
            } 
            else
            {
                return await base.CreateEntityAsync(value);
            }

        }

        //other methods
        public async Task<User> LogEntity(LoginViewModel value)
        {

            if (value.Password == String.Empty || value.Password == null)
            {
                return null;
            }

            var encryptedPassword = PasswordEncryptation.PasswordHash(value.Password);
            var logUser = await _persistence.Set<User>()
                .FirstOrDefaultAsync(data => data.Username == value.Username && data.Password == encryptedPassword);

            if (logUser != null)
            {
                return logUser;
            }

            return null;
        }

        public async Task<bool> UsernameExist(string username)
        {
            var userExists = await _persistence.Set<User>().FirstOrDefaultAsync(data => data.Username == username);
            return userExists != null;
        }
    }
}
