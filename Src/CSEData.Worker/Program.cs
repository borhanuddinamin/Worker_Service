using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSEData.Worker;
using Microsoft.EntityFrameworkCore;
using Persistent.Database;
using Serilog;
using Serilog.Events;

var configuration= new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json",false)
                  .AddEnvironmentVariables()
                  .Build();

var connectionString = configuration.GetConnectionString("DbConnection");
var migrationString=typeof(Worker).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

try
{

    IHost host = Host.CreateDefaultBuilder(args)
             .UseWindowsService()
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .UseSerilog()
             .ConfigureContainer<ContainerBuilder>(container =>
             {
                 container.RegisterModule(new WorkerModule(connectionString, migrationString));

             })
             .ConfigureServices(service =>
             {
                 service.AddHostedService<Worker>();
                 service.AddDbContext<ApplicationDbContext>(option =>
                        option.UseSqlServer(connectionString,
                         (x) => x.MigrationsAssembly(migrationString)
                         ));
             })
            .Build();


    Log.Information("Worker Application Running");
    await host.RunAsync();

}
catch(Exception ex)
{
    Log.Error(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}