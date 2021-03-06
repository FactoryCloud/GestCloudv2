﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public int? Code { get; set; }
        public string Name { get; set; }

        public virtual List<CompanyPaymentMethod> CompaniesPaymentMethod { get; set; }
        public virtual List<PurchaseOrder> purchaseOrders { get; set; }
        public virtual List<PurchaseDelivery> purchaseDeliveries { get; set; }
        public virtual List<PurchaseInvoice> purchaseInvoices { get; set; }
        public virtual List<SaleOrder> saleOrders { get; set; }
        public virtual List<SaleDelivery> saleDeliveries { get; set; }
        public virtual List<SaleInvoice> saleInvoices { get; set; }
    }
}
