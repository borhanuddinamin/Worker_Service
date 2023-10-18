using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistent.Entity
{
    public  class Price
    {

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public decimal LtpPrice { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public DateTime Time { get; set; }

    }
}
