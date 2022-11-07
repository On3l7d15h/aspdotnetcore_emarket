using Microsoft.AspNetCore.Http;
using SolutionEMarket.Core.Application.Helpers;
using SolutionEMarket.Core.Application.ViewModels.User;

namespace EMarket.Middlewares
{
    public class UserMiddleware
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserMiddleware(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public bool IsLogin()
        {
            var isUserLogged = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            return isUserLogged != null;
        }
    }
}
