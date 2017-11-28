﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("FK_PurchaseOrders_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_PurchaseOrders_ProviderID_Providers")]
        public int? ProviderID { get; set; }
        public virtual Provider provider { get; set; }
    }
}