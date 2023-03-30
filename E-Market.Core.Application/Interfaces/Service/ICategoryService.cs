using E_Market.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Service
{
    public interface ICategoryService : IGenericService<SaveCategoryViewModel, CategoryViewModel>
    {

    }
}
