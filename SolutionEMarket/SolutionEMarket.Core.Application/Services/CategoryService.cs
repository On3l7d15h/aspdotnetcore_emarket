using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using SolutionEMarket.Core.Application.Interfaces.Repositories;
using SolutionEMarket.Core.Application.Interfaces.Services;
using SolutionEMarket.Core.Application.ViewModels.Category;
using SolutionEMarket.Core.Domain.Entities;

namespace SolutionEMarket.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #region Get Methods

        public async Task<List<CategoryViewModel>> GetViewModel()
        {
            var categoryList = await _categoryRepository.GetEntitiesAsync();
            return categoryList
                    .Select(data => new CategoryViewModel
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Description = data.Description,
                    })
                    .ToList();
        }

        public async Task<SaveCategoryViewModel> GetSpecificSaveViewModel(int id)
        {
            var expectedCategory = await _categoryRepository.GetSpecificEntityAsync(id);

            if (expectedCategory != null)
            {
                return new SaveCategoryViewModel
                {
                    Id = expectedCategory.Id,
                    Name = expectedCategory.Name,
                    Description = expectedCategory.Description
                };
            }

            return new SaveCategoryViewModel();
        }

        #endregion

        #region Post Methods

        public async Task<SaveCategoryViewModel> CreateSaveViewModel(SaveCategoryViewModel newCategory)
        {
            var category = await _categoryRepository.CreateEntityAsync(
                new Category { 
                    Name = newCategory.Name,
                    Description = newCategory.Description
                }
            );

            SaveCategoryViewModel categoryvm = new()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return categoryvm;
        }

        #endregion

        #region Update Methods

        public async Task UpdateSaveViewModel(SaveCategoryViewModel actualCategory)
        {
            await _categoryRepository.UpdateEntityAsync(new Category
            {
                Id = actualCategory.Id,
                Name = actualCategory.Name,
                Description = actualCategory.Description
            });
        }

        #endregion 

        #region Delete Methods

        public async Task DeleteSaveViewModel(int id)
        {
            Category category = await _categoryRepository.GetSpecificEntityAsync(id);
            await _categoryRepository.DeleteEntityAsync(category);
        }

        #endregion 

    }
}
