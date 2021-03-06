﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class PurchaseInvoice
    {
        public int PurchaseInvoiceID { get; set; }

        public decimal PurchaseInvoiceFinalPrice { get; set; }

        public string Code { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("FK_PurchaseInvoices_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_PurchaseInvoices_ProviderID_Providers")]
        public int? ProviderID { get; set; }
        public virtual Provider provider { get; set; }

        [ForeignKey("FK_PurchaseInvoices_StoreID_Stores")]
        public int? StoreID { get; set; }
        public virtual Store store { get; set; }

        [ForeignKey("FK_PurchaseInvoices_PaymentMethodID_PaymentMethods")]
        public int? PaymentMethodID { get; set; }
        public virtual PaymentMethod paymentMethod { get; set; }

        public virtual List<PurchaseDelivery> PurchaseDeliveries { get; set; }
        public virtual List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
