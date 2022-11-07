using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using SolutionEMarket.Core.Application.ViewModels.Category;

namespace SolutionEMarket.Core.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<SaveCategoryViewModel, CategoryViewModel>
    {
    }
}
