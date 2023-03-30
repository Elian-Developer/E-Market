using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Service;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Infrastructure.Persistence.Repositories;
using E_Market.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace E_Market.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userServices;
        private readonly ValidateUserSession _validateUserSession;
 
        public UserController(IUserService userServices, ValidateUserSession validateUserSession)
        {
            _validateUserSession = validateUserSession;
            _userServices = userServices;
  
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(); //Login
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (_validateUserSession.HasUser()) //Autorizaciones
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UserViewModel userVm = await _userServices.Login(loginVm);  //3- Method to validate Login
            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Access date incorrect");
            }

            return View(loginVm);

        }

        public IActionResult LogOut() //LogOut
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }



        public IActionResult Register() // Register
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userVm) //Method to validate Register
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(userVm);
            }

            SaveUserViewModel user = await _userServices.Add(userVm);

            if (user == null)
            {
                ModelState.AddModelError("UsernameValidation", "This Username is in use Already.");
                return View(userVm);
            }
            else
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            //SaveUserViewModel user = await _userServices.CheckUser(userVm.UserName);

            //if (user != null)
            //{
            //    ViewData["userExistent"] = "This Username is in use Already.";
            //    return View(userVm);
            //}

            //await _userServices.Add(userVm);
            //return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
