using CSEData.Worker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Model
{
    public  class MarketModel
    {
        public long CompanyId { get; set; }
        public string StockCodeName { get; set; }
        public decimal LtpPrice { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }




    }
}
