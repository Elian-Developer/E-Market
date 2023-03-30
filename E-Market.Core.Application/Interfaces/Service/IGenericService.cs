using E_Market.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Service
{
    public interface IGenericService<SaveViewModel, ViewModel> 
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Edit(SaveViewModel vm);
        Task Delete(int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModels(int id);
    }
}
