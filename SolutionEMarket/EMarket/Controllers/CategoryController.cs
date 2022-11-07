using EMarket.Middlewares;
using Microsoft.AspNetCore.Mvc;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.Category;

namespace EMarket.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserMiddleware _userMiddleware;

        public CategoryController(ICategoryService categoryService, UserMiddleware userMiddleware)
        {
            _categoryService = categoryService;
            _userMiddleware = userMiddleware;
        }
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
            var categoryList = await _categoryService.GetViewModel();
            return View(categoryList);
        }

        #region Create Methods

        public async Task<IActionResult> Create()
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel actualCategory)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            if (ModelState.IsValid)
            {
                await _categoryService.CreateSaveViewModel(actualCategory);
                return RedirectToRoute(new
                {
                    Controller = "Category",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region Update Methods

        public async Task<IActionResult> Update(int id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            var specificCategory = await _categoryService.GetSpecificSaveViewModel(id);
            return View(specificCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveCategoryViewModel actualCategory)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            if (ModelState.IsValid)
            {
                await _categoryService.UpdateSaveViewModel(actualCategory);
                return RedirectToRoute(new
                {
                    Controller = "Category",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region Delete Methods

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            var specificCategory = await _categoryService.GetSpecificSaveViewModel(id);
            return View(specificCategory);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userMiddleware.IsLogin())
            {
                return RedirectToRoute(new
                {
                    Controller = "User",
                    Action = "Index"
                });
            }

            if (ModelState.IsValid)
            {
                await _categoryService.DeleteSaveViewModel(id);
                return RedirectToRoute(new
                {
                    Controller = "Category",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion
    }
}
