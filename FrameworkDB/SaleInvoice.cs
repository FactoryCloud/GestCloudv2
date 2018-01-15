using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class SaleInvoice
    {
        public int SaleInvoiceID { get; set; }

        public decimal SaleInvoiceFinalPrice { get; set; }

        public string Code { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("FK_SaleInvoices_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_SaleInvoices_ClientID_Clients")]
        public int? ClientID { get; set; }
        public virtual Client client { get; set; }

        [ForeignKey("FK_SaleInvoices_StoreID_Stores")]
        public int? StoreID { get; set; }
        public virtual Store store { get; set; }

        public virtual List<SaleDelivery> SaleDeliveries { get; set; }
        public virtual List<SaleOrder> SaleOrders { get; set; }
    }
}
