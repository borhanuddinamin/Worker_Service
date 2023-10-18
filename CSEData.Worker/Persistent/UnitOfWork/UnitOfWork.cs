using Persistent.Database;
using Persistent.Repository;
using Persistent.Repository.RepositoryInterface;

namespace Persistent.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;
        public ICompanyRepository CompanyRepository { get; }
        public UnitOfWork() { }
        public UnitOfWork(ApplicationDbContext dbContext, 
                         ICompanyRepository _CompanyRepository)
        {
            _context=dbContext;

            CompanyRepository = _CompanyRepository;
        }

        public IPriceRepository PriceRepository {
            get
            {
                return new PriceRepository(_context);
            }
        }

        public void Dispose()
        {
           
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
