using CSEData.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Service
{
    public interface IMarketService
    {
        Task<List<MarketModel>> IsDuplicate(List<MarketModel> models);
        Task<bool> Create(List<MarketModel> models);
    }
}
