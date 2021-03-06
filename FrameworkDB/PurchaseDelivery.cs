﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class PurchaseDelivery
    {
        public int PurchaseDeliveryID { get; set; }

        public decimal PurchaseDeliveryFinalPrice { get; set; }

        public string Code { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("FK_PurchaseDeliveries_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_PurchaseDeliveries_ProviderID_Providers")]
        public int? ProviderID { get; set; }
        public virtual Provider provider { get; set; }

        [ForeignKey("FK_PurchaseDeliveries_StoreID_Stores")]
        public int? StoreID { get; set; }
        public virtual Store store { get; set; }

        [ForeignKey("FK_PurchaseDeliveries_PurchaseInvoiceID_PurchaseInvoices")]
        public int? PurchaseInvoiceID { get; set; }
        public virtual PurchaseInvoice purchaseInvoice { get; set; }

        [ForeignKey("FK_PurchaseDeliveries_PaymentMethodID_PaymentMethods")]
        public int? PaymentMethodID { get; set; }
        public virtual PaymentMethod paymentMethod { get; set; }

    }
}
