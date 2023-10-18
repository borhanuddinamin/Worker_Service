using Autofac;
using CSEData.Worker.Service;
using Persistent.Database;
using Persistent.Repository;
using Persistent.Repository.RepositoryInterface;
using Persistent.UnitOfWork;

namespace CSEData.Worker
{
    internal class WorkerModule : Module
    {

        public readonly string _ConnectionString;
        public readonly string migrationString;
        public WorkerModule(string ConnectionString, string migrationString)
        {
            _ConnectionString=ConnectionString;
            this.migrationString = migrationString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _ConnectionString)
                .WithParameter("migrationString", this.migrationString)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As
                <IApplicationDbContext>()
                .WithParameter("connectionString", _ConnectionString)
                //.WithParameter("migrationString", this.migrationString)
            .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PriceRepository>().As<IPriceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
               .InstancePerLifetimeScope();

            builder.RegisterType<MarketService>().As<IMarketService>()
                .InstancePerLifetimeScope();

            base.Load(builder);

        }
    }
}
