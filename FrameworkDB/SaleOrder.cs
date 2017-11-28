﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class SaleOrder
    {
        public int SaleOrderID { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("FK_SaleOrders_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_SaleOrders_ClientID_Clients")]
        public int? ClientID { get; set; }
        public virtual Client client { get; set; }
    }
}