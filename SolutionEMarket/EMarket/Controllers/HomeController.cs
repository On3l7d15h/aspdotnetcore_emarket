using EMarket.Middlewares;
using EMarket.Models;
using Microsoft.AspNetCore.Mvc;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.User;
using System.Diagnostics;
using SolutionEMarket.Core.Application.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using SolutionEMarket.Core.Application.Helpers;

namespace EMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserMiddleware _userMiddleware;

        public HomeController(ILogger<HomeController> logger, IUserService userService, UserMiddleware userMiddleware, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _userService = userService;
            _userMiddleware = userMiddleware;
            _productService = productService;
            _categoryService = categoryService;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }
            ProductFilterViewModel prodmv = new();
            ViewBag.Categories = await _categoryService.GetViewModel();
            var products = await _productService.GetFilteredProducts(prodmv);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductFilterViewModel filter)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            ViewBag.Categories = await _categoryService.GetViewModel();
            var products = await _productService.GetFilteredProducts(filter);
            return View(products);
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(int Id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            var products = await _productService.GetSpecificSaveViewModel(Id);
            ViewBag.User = await _userService.GetSpecificSaveViewModel(products.UserId);
            ViewBag.Categories = await _categoryService.GetSpecificSaveViewModel(products.CategoryId);
            return View(products);
        }

        

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}