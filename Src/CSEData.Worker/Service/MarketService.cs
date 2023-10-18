using CSEData.Worker.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Persistent.Entity;
using Persistent.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Service
{
    public class MarketService : IMarketService
    {
        public readonly IUnitOfWork _dbContext;

        public MarketService(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MarketModel>> IsDuplicate(List<MarketModel> models)
        {
            List<MarketModel> marketModels = new List<MarketModel>();
            var result = _dbContext.CompanyRepository.GetAll();
            foreach (var data in models)
            {
                var id = data.CompanyId = result.Where(x => x.StockCodeName == data.StockCodeName)
                                    .Select(x => x.Id).FirstOrDefault();

                if (id < 1 || id == null)
                {

                    CreateStockCodeName(data.StockCodeName);
                    var newData = _dbContext.CompanyRepository.GetAll();
                    data.CompanyId = newData.Where(x => x.StockCodeName == data.StockCodeName)
                                    .Select(x => x.Id).FirstOrDefault();
                    marketModels.Add(data);
                }
                else
                {
                    data.CompanyId = id;
                    marketModels.Add(data);
                }



            }
            return marketModels;
        }

        public async Task<bool> Create(List<MarketModel> models)
        {

            List<Price> prices = new List<Price>();
            var MarketData = await IsDuplicate(models);
            foreach (var model in MarketData)
            {
                Price price = new Price();

                try
                {
                    price.CompanyId = model.CompanyId;
                    price.LtpPrice = model.LtpPrice;
                    price.Open = model.Open;
                    price.High = model.High;
                    price.Low = model.Low;
                    price.Time = DateTime.Now;
                    prices.Add(price);
                }
                catch
                {
                    continue;
                }




            }
            _dbContext.PriceRepository.AddRange(prices);
            _dbContext.Save();
            return true;
        }




        public void CreateStockCodeName(string Name)
        {
            Company company = new Company()
            {
                StockCodeName = Name
            };

            _dbContext.CompanyRepository.Add(company);
            _dbContext.Save();

        }
    }
}
