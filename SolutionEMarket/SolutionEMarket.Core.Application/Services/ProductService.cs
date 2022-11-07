using Microsoft.AspNetCore.Http;
using SolutionEMarket.Core.Application.Helpers;
//added
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.Product;
using SolutionEMarket.Core.Application.ViewModels.User;
using SolutionEMarket.Core.Domain.Entities;

namespace SolutionEMarket.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productService;
        private readonly ICategoryRepository _categoryService;
        private readonly IHttpContextAccessor _httpContext;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryService, IHttpContextAccessor httpContext)
        {
            _productService = productRepository;
            _categoryService = categoryService;
            _httpContext = httpContext;
        }

        #region Get Methods

        public async Task<List<ProductViewModel>> GetViewModel()
        {
            var listProducts = await _productService.GetEntitiesAsync();
            var listCategories = await _categoryService.GetEntitiesAsync();
            var actualUser = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            return listProducts
                .Where(data => data.UserId == actualUser.Id)
                .Select(data => new ProductViewModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    ImagePath = data.ImagePath1,
                    UserId = data.UserId,
                    CategoryId = data.CategoryId,
                    CategoryName = listCategories.Single(x => x.Id == data.CategoryId).Name,
                    Price = data.Price,
                })
                .ToList();
        }


        public async Task<SaveProductViewModel> GetSpecificSaveViewModel(int id)
        {
            var product = await _productService.GetSpecificEntityAsync(id);
            return new SaveProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImagePath1 = product.ImagePath1,
                ImagePath2 = product.ImagePath2,
                ImagePath3 = product.ImagePath3,
                ImagePath4 = product.ImagePath4,
                Created = product.Created.ToString(),
                UserId = product.UserId,
                CategoryId = product.CategoryId,
                Price = product.Price,
            };
        }

        //added
        public async Task<ProductViewModel> GetSpecificViewModel(int id)
        {
            var product = await _productService.GetSpecificEntityAsync(id);
            var listCategories = await _categoryService.GetEntitiesAsync();
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImagePath = product.ImagePath1,
                UserId = product.UserId,
                CategoryId = product.CategoryId,
                CategoryName = listCategories.Single(x => x.Id == product.CategoryId).Name,
                Price = product.Price,
            };
        }

        public async Task<List<ProductViewModel>> GetFilteredProducts(ProductFilterViewModel filter)
        {
            var list = await _productService.GetEntitiesAsync();
            var listCategories = await _categoryService.GetEntitiesAsync();
            var actualUser = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            var filteredList = list
                .Where(data => data.UserId != actualUser.Id)
                .Select(data => new ProductViewModel 
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    ImagePath = data.ImagePath1,
                    UserId = data.UserId,
                    CategoryId = data.CategoryId,
                    CategoryName = listCategories.Single(x => x.Id == data.CategoryId).Name,
                    Price = data.Price,
                })
                .ToList();

            if (filter.ProductName != null && filter.ProductName != "")
            {
                return filteredList.Where(x => x.Name.Contains(filter.ProductName)).ToList();
            }

            if (filter.CategoryId != null && filter.CategoryId.Length > 0)
            {
                if (filter.CategoryId.Contains(0))
                {
                    return filteredList;
                }
                else
                {
                    List<ProductViewModel> filtered = new();
                    foreach (var id in filter.CategoryId)
                    {
                        filtered.AddRange(filteredList.Where(x => x.CategoryId == id));
                    }

                    return filtered;
                }
            }

            return filteredList;
        }

        #endregion

        #region Post Methods

        public async Task<SaveProductViewModel> CreateSaveViewModel(SaveProductViewModel newProduct)
        {
            var loggedUser = _httpContext.HttpContext.Session.Get<UserViewModel>("user");

            Product product = new Product
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                ImagePath1 = newProduct.ImagePath1,
                ImagePath2 = newProduct.ImagePath2,
                ImagePath3 = newProduct.ImagePath3,
                ImagePath4 = newProduct.ImagePath4,
                Price = newProduct.Price,
                UserId = loggedUser.Id,
                CategoryId = newProduct.CategoryId
            };
            
            product = await _productService.CreateEntityAsync(product);

            SaveProductViewModel productvm = new SaveProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImagePath1 = product.ImagePath1,
                ImagePath2 = product.ImagePath2,
                ImagePath3 = product.ImagePath3,
                ImagePath4 = product.ImagePath4,
                Price = product.Price,
                UserId = product.UserId,
                CategoryId = product.CategoryId
            };

            return productvm;
        }

        #endregion

        #region Update Methods

        public async Task UpdateSaveViewModel(SaveProductViewModel updateProduct)
        {
            Product product = await _productService.GetSpecificEntityAsync(updateProduct.Id);
            product.Id = updateProduct.Id;
            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.ImagePath1 = updateProduct.ImagePath1;
            product.ImagePath2 = updateProduct.ImagePath2;
            product.ImagePath3 = updateProduct.ImagePath3;
            product.ImagePath4 = updateProduct.ImagePath4;
            product.Price = updateProduct.Price;
            product.UserId = updateProduct.UserId;
            product.CategoryId = updateProduct.CategoryId;

            await _productService.UpdateEntityAsync(product);
        }

        #endregion

        #region Delete Methods

        public async Task DeleteSaveViewModel(int id)
        {
            Product product = await _productService.GetSpecificEntityAsync(id);
            await _productService.DeleteEntityAsync(product);
        }

        #endregion
    }
}
