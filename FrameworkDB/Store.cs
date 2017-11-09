using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Store
    {
        public int StoreID { get; set; }
        public int? Code { get; set; }
        public string Name { get; set; }

        public virtual List<CompanyStore> CompaniesStores { get; set; }
        public virtual List<Movement> Movements { get; set; }
    }
}
