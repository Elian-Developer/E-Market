using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repository;
using E_Market.Core.Application.Interfaces.Service;
using E_Market.Core.Application.ViewModels.Adverstisements;
using E_Market.Core.Application.ViewModels.Category;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class AdsService : IAdsService
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public AdsService(IAdsRepository adsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adsRepository = adsRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

        }

        public virtual async Task<SaveAdsViewModel> Add(SaveAdsViewModel vm)
        {
            Adverstisements ads = new();
            ads.Name = vm.Name;
            ads.Description = vm.Description;
            ads.Price = vm.Price;
            ads.ImageUrl = vm.ImageUrl;
            ads.CategoryId = vm.CategoryId;
            ads.UserId = vm.UserId;

            ads = await _adsRepository.AddAsync(ads);

            SaveAdsViewModel adsVm = new();

            adsVm.Id = ads.Id;
            adsVm.Name = ads.Name;
            adsVm.Description = ads.Description;
            adsVm.Price = ads.Price;
            adsVm.ImageUrl = ads.ImageUrl;
            adsVm.CategoryId = ads.CategoryId;
            adsVm.UserId = ads.UserId;

            return adsVm;
        }

        public virtual async Task Edit(SaveAdsViewModel vm)
        {
            Adverstisements ads = await _adsRepository.GetByIdAsync(vm.Id);
            ads.Id = vm.Id;
            ads.Name = vm.Name;
            ads.Description = vm.Description;
            ads.Price = vm.Price;
            ads.ImageUrl = vm.ImageUrl;
            ads.CategoryId = vm.CategoryId;
            ads.UserId = vm.UserId;

            await _adsRepository.EditAsync(ads);
        }

        public virtual async Task Delete(int id)
        {
            var ads = await _adsRepository.GetByIdAsync(id);
            await _adsRepository.DeleteAsync(ads);
        }

        public async Task<List<AdsViewModel>> GetAllViewModel()
        {
            var adsList = await _adsRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return adsList.Where(ads => ads.UserId == _userViewModel.Id).Select(ads => new AdsViewModel
                {
                Id = ads.Id,
                Name = ads.Name,
                Description = ads.Description,
                Price = ads.Price,
                ImageUrl = ads.ImageUrl,
                CategoryId = ads.CategoryId,
                UserId = ads.UserId,
                Category = ads.Category.Name

            }).ToList();
        }

        public async Task<List<AdsViewModel>> GetAllViewModelWithFilters(FilterAdsViewModel filter)
        {
            var adsList = await _adsRepository.GetAllWithIncludeAsync(new List<string> { "Category", "User" });

            var ListViewModels = adsList.Where(ads => ads.UserId != _userViewModel.Id)
                .Select(ads => new AdsViewModel
            {
                Id = ads.Id,
                Name = ads.Name,
                Description = ads.Description,
                Price = ads.Price,
                ImageUrl = ads.ImageUrl,
                CategoryId = ads.CategoryId,
                UserId = ads.UserId,
                Category = ads.Category.Name,
                AdsUserName = $"{ads.User.Name} {ads.User.LastName}",
                AdsCreated = ads.Created,
                AdsLastModified = ads.LastModified,
                AdsUserEmail = ads.User.Email,
                AdsUserPhone = ads.User.Phone,
            }).ToList();

            if(filter.AdsName != null)
            {
                ListViewModels = ListViewModels.Where(ads => ads.Name!.Contains(filter.AdsName)).ToList();
            }
            else if(filter.CategoryId != null)
            {
                ListViewModels = ListViewModels.Where(category => category.CategoryId == filter.CategoryId.Value).ToList();
            }
            
            return ListViewModels;
        }

        public async Task<AdsViewModel> GetDetailsAdsViewModel(int id)
        {
            var ads = await _adsRepository.GetAllWithIncludeAsync(id, new List<string> { "Category", "User" });
            
            AdsViewModel vm = new()
            {
                Id = ads.Id,
                Name = ads.Name,
                Description = ads.Description,
                Price = ads.Price,
                ImageUrl = ads.ImageUrl,
                CategoryId = ads.CategoryId,
                UserId = ads.UserId,
                Category = ads.Category.Name,
                AdsUserName = $"{ads.User.Name} {ads.User.LastName}",
                AdsCreated = ads.Created,
                AdsLastModified = ads.LastModified,
                AdsUserEmail = ads.User.Email,
                AdsUserPhone = ads.User.Phone,
            };

            return vm;
        }

        public virtual async Task<SaveAdsViewModel> GetByIdSaveViewModels(int id)
        {
            var ads = await _adsRepository.GetByIdAsync(id);

            SaveAdsViewModel vm = new();
            vm.Id = ads.Id;
            vm.Name = ads.Name;
            vm.ImageUrl = ads.ImageUrl;
            vm.Price = ads.Price;
            vm.Description = ads.Description;
            vm.CategoryId = ads.CategoryId;
            vm.UserId = ads.UserId;

            return vm;
        }
    }
}
