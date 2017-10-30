﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }

        public virtual List<CompanyStore> CompaniesStores { get; set; }
        public virtual List<StockAdjust> StockAdjusts { get; set; }
        public virtual List<SaleDelivery> SaleDeliveries { get; set; }
        public virtual List<SaleInvoice> SaleInvoices { get; set; }
        public virtual List<PurchaseDelivery> PurchaseDeliveries { get; set; }
        public virtual List<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}