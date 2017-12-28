using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Company
    {
        public int CompanyID { get; set; }
        public int? Code { get; set; }
        public string Name { get; set; }
        public int PeriodOption { get; set; }

        [ForeignKey("[FK_Companies_FiscalYearID_FiscalYears]")]
        public int FiscalYearID { get; set; }
        public virtual FiscalYear fiscalYear { get; set; }
        

        public virtual List<CompanyStore> CompaniesStores { get; set; }
        public virtual List<StockAdjust> StockAdjusts { get; set; }
        public virtual List<SaleDelivery> SaleDeliveries { get; set; }
        public virtual List<SaleInvoice> SaleInvoices { get; set; }
        public virtual List<SaleOrder> SaleOrders { get; set; }
        public virtual List<PurchaseDelivery> PurchaseDeliveries { get; set; }
        public virtual List<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual List<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual List<TaxType> TaxTypes { get; set; }
        public virtual List<FiscalYear> FiscalYears { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
