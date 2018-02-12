using FrameworkDB.V1;
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
    public class StoreTransfersView: ItemsView
    {
        List<StoreTransfer> items;

        public StoreTransfersView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Almacén Origen", typeof(string));
            dt.Columns.Add("Almacén Destino", typeof(string));
        }

        public StoreTransfersView(List<StoreTransfer> Documents) : this()
        {
            items = Documents;
        }

        override public void UpdateTable()
        {
            items = db.StoreTransfers.Include(e => e.storeFrom).Include(e => e.storeTo).ToList();

            dt.Clear();
            foreach (StoreTransfer storeTransfer in items)
            {
                dt.Rows.Add(storeTransfer.StoreTransferID, $"{String.Format("{0:dd/MM/yyyy}", storeTransfer.Date)}", storeTransfer.storeFrom.Name, storeTransfer.storeTo.Name);
            }
        }

        public void SetDocuments(List<StoreTransfer> Documents)
        {
            items = Documents;
        }

        public StoreTransfer GetStoreTransfer(int num)
        {
            return db.StoreTransfers.Where(p => p.StoreTransferID == num).First();
        }
    }
}
