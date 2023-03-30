using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repository;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using E_Market.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            return await base.AddAsync(entity);
        }

        //1- Repositorio para verificar los datos del login
        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypy = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == loginVm.UserName && user.Password == passwordEncrypy);
            return user;
        }

        //public async Task<User> CheckUserAsync(string userName)
        //{
        //    User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.UserName == userName);

        //    return user;
        //}

    }
}
