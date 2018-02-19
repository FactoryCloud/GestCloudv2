using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class SaleDelivery
    {
        public int SaleDeliveryID { get; set; }

        public decimal SaleDeliveryFinalPrice { get; set; }

        public string Code { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("FK_SaleDeliveries_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_SaleDeliveries_ClientID_Clients")]
        public int? ClientID { get; set; }
        public virtual Client client { get; set; }

        [ForeignKey("FK_SaleDeliveries_StoreID_Stores")]
        public int? StoreID { get; set; }
        public virtual Store store { get; set; }

        [ForeignKey("FK_SaleDeliveries_SaleInvoiceID_SaleInvoices")]
        public int? SaleInvoiceID { get; set; }
        public virtual SaleInvoice saleInvoice { get; set; }

        [ForeignKey("FK_SaleDeliveries_PaymentMethodID_PaymentMethods")]
        public int? PaymentMethodID { get; set; }
        public virtual PaymentMethod paymentMethod { get; set; }
    }
}
