using E_Market.Core.Application.Interfaces.Service;
using E_Market.Core.Application.ViewModels.Adverstisements;
using Microsoft.AspNetCore.Mvc;

namespace E_Market.Controllers
{
    public class AdsController : Controller
    {
        private readonly IAdsService _adsService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public AdsController(IAdsService adsService, ICategoryService categoryService, IUserService userService)
        {
            _adsService = adsService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _adsService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            SaveAdsViewModel vm = new();
            vm.Users = await _userService.GetAllViewModel();
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveAds", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdsViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Users = await _userService.GetAllViewModel();
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveAds", vm);
            }

            SaveAdsViewModel adsVm = await _adsService.Add(vm);

            if(adsVm.Id != 0 || adsVm != null)
            {
                adsVm.ImageUrl = UploadFile(vm.File, adsVm.Id);
                await _adsService.Edit(adsVm);
            }

            return RedirectToRoute(new { controller = "Ads", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveAdsViewModel vm = await _adsService.GetByIdSaveViewModels(id);
            vm.Users = await _userService.GetAllViewModel();
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveAds", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Users = await _userService.GetAllViewModel();
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveAds", vm);
            }

            SaveAdsViewModel adsVm = await _adsService.GetByIdSaveViewModels(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, adsVm.Id, true, adsVm.ImageUrl);

            await _adsService.Edit(vm);
            return RedirectToRoute(new { controller = "Ads", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View("Delete", await _adsService.GetByIdSaveViewModels(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _adsService.Delete(id);
            return RedirectToRoute(new { controller = "Ads", action = "Index" });

        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl = "")
        {
            if(isEditMode && file == null)
            {
                return imageUrl;
            }

            string basePath = $"/Images/Adverstisements/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Creat efolder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if(isEditMode)
            {
                string[] oldImagePart = imageUrl.Split('/');
                string oldImageName = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{filename}";
        }
    }
}
