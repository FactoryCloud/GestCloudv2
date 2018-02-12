using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferMenu.Controller
{
    public partial class CT_StoreTransferMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public StoreTransfer storeTransfer;

        public CT_StoreTransferMenu()
        {
            itemsView = new StoreTransfersView();
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_STT_Menu();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_STT_Menu();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_STT_Menu();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Purchases.Controller.CT_Purchases();
        }

        public override Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new StoreTransferItem.StoreTransferItem_New.Controller.CT_STT_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new StoreTransferItem.StoreTransferItem_Load.Controller.CT_STT_Item_Load(storeTransfer, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new StoreTransferItem.StoreTransferItem_Load.Controller.CT_STT_Item_Load(storeTransfer, 1);
        }

        override public bool SelectedItem()
        {
            return storeTransfer != null;
        }

        override public void SetItem(int num)
        {
            storeTransfer = db.StoreTransfers.Where(c => c.StoreTransferID== num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

