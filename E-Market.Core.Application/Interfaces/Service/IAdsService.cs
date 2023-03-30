using E_Market.Core.Application.ViewModels.Adverstisements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Service
{
    public interface IAdsService : IGenericService<SaveAdsViewModel, AdsViewModel>
    {
        Task<AdsViewModel> GetDetailsAdsViewModel(int id);
        Task<List<AdsViewModel>> GetAllViewModelWithFilters(FilterAdsViewModel filter);
        //Task<List<AdsViewModel>> GetAllAdsViewModelOtherUsers();
    }
}
