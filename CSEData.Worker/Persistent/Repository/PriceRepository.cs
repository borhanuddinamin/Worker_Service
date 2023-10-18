using Microsoft.EntityFrameworkCore;
using Persistent.Database;
using Persistent.Entity;
using Persistent.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistent.Repository
{
    public class PriceRepository : Repository<Price, long>, IPriceRepository
    {
        public PriceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
