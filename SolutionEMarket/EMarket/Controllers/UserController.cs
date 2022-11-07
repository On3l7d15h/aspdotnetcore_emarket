using EMarket.Middlewares;
using Microsoft.AspNetCore.Mvc;
using SolutionEMarket.Core.Application.Helpers;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.User;

namespace EMarket.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserMiddleware _userMiddleware; 

        public UserController(IUserService userService, UserMiddleware userMiddleware)
        {
            _userService = userService;
            _userMiddleware = userMiddleware;
        }

        #region Both Index and login 
        public IActionResult Index()
        {
            if (_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginvm)
        {
            if (_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            if (ModelState.IsValid && loginvm != null)
            {
                var login = await _userService.GetLogEntity(loginvm);

                if (login != null)
                {
                    HttpContext.Session.Set<UserViewModel>("user", login);
                    return RedirectToRoute(new
                    {
                        Controller = "Home",
                        Action = "Index"
                    });
                }
                else
                {
                    ModelState.AddModelError("Validate Login", "Sorry, Error Credentials");
                }

            }

            return View(loginvm);
        }

        #endregion

        #region Register
        public IActionResult Register()
        {
            if (_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel newUser)
        {
            if (_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            }

            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            if (await _userService.isUsernameExist(newUser.Username))
            {
                ModelState.AddModelError("Username Exists", "Oops!, you need to pick up another username");
                return View(newUser);
            }

            await _userService.CreateSaveViewModel(newUser);
            return RedirectToRoute(new
            {
                Controller = "User",
                Action = "Index"
            });

        }
        #endregion

        #region Log Out
        
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new
            {
                Controller = "User",
                Action = "Index"
            });
        }

        #endregion

    }
}
