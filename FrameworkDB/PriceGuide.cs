using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class PriceGuide
    {
        public Decimal Sell { get; set; }
        public Decimal Low { get; set; }
        public Decimal Lowex { get; set; }
        public Decimal LowFoil { get; set; }
        public Decimal AVG { get; set; }
        public Decimal Trend { get; set; }
    }
}
