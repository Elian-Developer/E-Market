using E_Market.Core.Application.Interfaces.Repository;
using E_Market.Core.Domain.Entities;
using E_Market.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class AdsRepository : GenericRepository<Adverstisements>, IAdsRepository
    {
        private readonly ApplicationContext _dbContext;

        public AdsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
