using E_Market.Core.Application.Interfaces.Repository;
using E_Market.Core.Application.Interfaces.Service;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            List<User> Users = await _userRepository.GetAllAsync();
            User User = Users.FirstOrDefault(user => user.UserName == vm.UserName);

            if(User != null)
            {
                return null;
            }

            User user = new();
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.UserName = vm.UserName;
            user.Password = vm.Password;

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.LastName = user.LastName;
            userVm.Email = user.Email;
            userVm.Phone = user.Phone;
            userVm.UserName = user.UserName;
            userVm.Password = user.Password;

            return userVm;
        }


        public async Task Edit(SaveUserViewModel vm)
        {
            User user = new();
            user.Id = vm.Id;    
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.UserName = vm.UserName;
            user.UserName = vm.Password;

            await _userRepository.EditAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModels(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.Phone = user.Phone;
            vm.UserName = user.UserName;
            vm.Password = user.Password;

            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var UserList = await _userRepository.GetAllAsync();

            return UserList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password,

            }).ToList();
        }

        //2- servicio para el login
        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _userRepository.LoginAsync(loginVm);

            if(user == null)
            {
                return null;
            }

            UserViewModel userVm = new();
            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.LastName = user.LastName;
            userVm.Email = user.Email;
            userVm.Phone = user.Phone;
            userVm.UserName = user.UserName;
            userVm.UserName = user.Password;

            return userVm;
        }

        //public async Task<SaveUserViewModel> CheckUser(string userName)
        //{
        //    User user = await _userRepository.CheckUserAsync(userName);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    SaveUserViewModel userVm = new();
        //    userVm.Id = user.Id;
        //    userVm.Name = user.Name;
        //    userVm.LastName = user.LastName;
        //    userVm.Email = user.Email;
        //    userVm.Phone = user.Phone;
        //    userVm.UserName = user.UserName;
        //    userVm.UserName = user.Password;

        //    return userVm;
        //}

    }
}
