using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Shortcuts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferItem.StoreTransferItem_New.Controller
{
    public partial class CT_STT_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public StoreTransfer storeTransfer;

        public CT_STT_Item_New():base()
        {
            storeTransfer = new StoreTransfer();
            Information["operationType"] = 3;
            GetLastCode();
            List<CompanyStore> Stores = db.CompaniesStores.Where(s => s.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID).Include(s => s.store).ToList();
            storeTransfer.storeFrom = Stores[0].store;
            storeTransfer.storeTo = Stores[1].store;
        }

        public void CleanPurchaseCode()
        {
            storeTransfer.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            storeTransfer.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_STT_Item_New_StoreTransfer();
                    break;

                case 2:
                    MC_Page = new View.MC_STT_Item_New_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_STT_Item_New_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_STT_Item_New_StoreTransfer();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_STT_Item_New_StoreTransfer();
        }

        
        public override void SetStoreFrom(int num)
        {
            storeTransfer.StoreFromID = num;
            storeTransfer.storeFrom = db.Stores.Where(s => s.StoreID == num).First();
        }

        public override void SetStoreTo(int num)
        {
            storeTransfer.StoreToID = num;
            storeTransfer.storeTo = db.Stores.Where(s => s.StoreID == num).First();
        }

        public override string GetCode()
        {
            return storeTransfer.Code;
        }

        public override int GetDocumentID()
        {
            return storeTransfer.StoreTransferID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("StoreTransfer")).First();
        }

        public override Store GetStore()
        {
            return storeTransfer.storeFrom;
        }

        public override Store GetStoreFrom()
        {
            return storeTransfer.storeFrom;
        }

        public override Store GetStoreTo()
        {
            return storeTransfer.storeTo;
        }

        override public void GetLastCode()
        {
            if (db.StoreTransfers.ToList().Count > 0)
            {
                lastCode = db.StoreTransfers.OrderBy(u => u.StoreTransferID).Last().StoreTransferID + 1;
            }
            else
            {
                lastCode = 1;
            }

            storeTransfer.Code = lastCode.ToString();
        }

        public override ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Traspaso entre Almacenes (Nuevo)",
                Controller = this

            };
        }

        override public void MD_MovementAdd()
        {
            View.FW_STT_Item_New_Movements floatWindow = new View.FW_STT_Item_New_Movements(Information["operationType"], movements);
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_STT_Item_New_Movements floatWindow = new View.FW_STT_Item_New_Movements(Information["operationType"], movements, new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<StoreTransfer> purchaseDeliveries = db.StoreTransfers.ToList();
            foreach (var item in purchaseDeliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanPurchaseCode();
                    return true;
                }
            }
            storeTransfer.Code = code;
            base.CodeExist(code);
            return false;
        }

        override public void SaveDocument()
        {
            /*storeTransfer.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            storeTransfer.ProviderID = provider.ProviderID;
            storeTransfer.store = db.Stores.Where(s => s.StoreID == store.StoreID).First();
            storeTransfer.StoreTransferFinalPrice = documentContent.PurchaseFinalPrice;
            db.StoreTransfers.Add(storeTransfer);*/
            db.SaveChanges();

            base.SaveDocument();
        }

        override public void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    if (Information["fieldEmpty"] == 1)
                    {
                        MessageBoxResult result = MessageBox.Show("Usted ha realizado cambios, ¿Esta seguro que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new StoreTransferMenu.Controller.CT_StoreTransferMenu();
                    break;
            }
        }
    }
}
