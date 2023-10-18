using Persistent.Repository.RepositoryInterface;

namespace Persistent.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public ICompanyRepository CompanyRepository { get; }
        public IPriceRepository PriceRepository { get; }

        void Save();

    }
}
