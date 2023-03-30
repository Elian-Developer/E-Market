using E_Market.Core.Application.Interfaces.Service;
using E_Market.Core.Application.Services;
using E_Market.Core.Application.ViewModels.Adverstisements;
using E_Market.Core.Application.ViewModels.Category;
using E_Market.Middlewares;
using E_Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IAdsService _adsService;
        private readonly ICategoryService _categoryService;

        public HomeController(ValidateUserSession validateUserSession, IAdsService adsService, ICategoryService categoryService)
        {
            _validateUserSession = validateUserSession;
            _adsService = adsService;
            _categoryService = categoryService;

        }

        public async Task <IActionResult> Index(FilterAdsViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            FilterAdsViewModel cvm = new();
            cvm.Categories = await _categoryService.GetAllViewModel();
            ViewBag.Categories = await _categoryService.GetAllViewModel();

            return View(await _adsService.GetAllViewModelWithFilters(vm));
        }

     
        public async Task<IActionResult> Details(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            //await _adsService.GetDetailsAdsViewModel(id);

            return View(await _adsService.GetDetailsAdsViewModel(id));
        }

    }
}