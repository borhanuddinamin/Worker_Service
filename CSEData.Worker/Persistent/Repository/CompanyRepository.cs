
using Microsoft.EntityFrameworkCore;
using Persistent.Database;
using Persistent.Entity;
using Persistent.Repository.RepositoryInterface;

namespace Persistent.Repository
{
    public class CompanyRepository : Repository<Company, long>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
