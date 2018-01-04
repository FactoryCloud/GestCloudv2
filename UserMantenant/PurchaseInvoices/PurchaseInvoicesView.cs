﻿using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class PurchaseInvoicesView:ItemsView
    {
        List<PurchaseInvoice> items { get; set; }

        public PurchaseInvoicesView()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
        }

        override public void UpdateTable()
        {
            items = db.PurchaseInvoices.Include(e => e.provider.entity).ToList();

            dt.Clear();
            foreach (PurchaseInvoice item in items)
            {
                dt.Rows.Add(item.PurchaseInvoiceID, item.provider.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", item.Date)}");
            }
        }
    }
}