using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.ViewModels.User;

namespace E_Market.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if(userViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
