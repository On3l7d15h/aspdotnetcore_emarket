using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using SolutionEMarket.Core.Application.ViewModels.Category;
using SolutionEMarket.Core.Application.ViewModels.Product;

namespace SolutionEMarket.Core.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<SaveProductViewModel, ProductViewModel>
    {
        Task<List<ProductViewModel>> GetFilteredProducts(ProductFilterViewModel filter);
        Task<ProductViewModel> GetSpecificViewModel(int id);
    }
}
