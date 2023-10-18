using Autofac;
using Autofac.Core.Lifetime;
using CSEData.Worker.MarketScrap;
using CSEData.Worker.Model;
using CSEData.Worker.Service;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace CSEData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public ILifetimeScope _Scope;
       
        public Worker(ILogger<Worker> logger,ILifetimeScope scope)
        {
            _logger = logger;
            _Scope = scope;
            


        }
   

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {

               
                
                if (ScrapData.IsMarketOpen())
                {
                    List<MarketModel> marketData=await ScrapData.MarketData();
                    var _creatService = _Scope.Resolve<IMarketService>();
                    bool IsCreat = await _creatService.Create(marketData);

                };
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(58000, stoppingToken);
                
            }
            
        }

        
    }
       

   


}
