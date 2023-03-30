using E_Market.Core.Application.Interfaces;
using E_Market.Core.Application.Interfaces.Repository;
using E_Market.Core.Application.Interfaces.Service;
using E_Market.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAdsService, AdsService>();
            #endregion
        }
    }
}
