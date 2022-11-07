using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<List<ViewModel>> GetViewModel();
        Task<SaveViewModel> GetSpecificSaveViewModel(int id);
        Task<SaveViewModel> CreateSaveViewModel(SaveViewModel newProduct);
        Task UpdateSaveViewModel(SaveViewModel updateProduct);
        Task DeleteSaveViewModel(int id);
    }
}
