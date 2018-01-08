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

        public virtual List<PurchaseDelivery> PurchaseDeliveries { get; set; }
        public virtual List<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual List<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual List<SaleDelivery> SaleDeliveries { get; set; }
        public virtual List<SaleInvoice> SaleInvoices { get; set; }
        public virtual List<SaleOrder> SaleOrders { get; set; }
    }
}
