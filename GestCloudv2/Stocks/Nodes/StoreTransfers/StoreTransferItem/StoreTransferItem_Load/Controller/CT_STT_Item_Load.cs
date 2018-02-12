using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using FrameworkView.V1;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferItem.StoreTransferItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_STT_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public StoreTransfer storeTransfer;

        public CT_STT_Item_Load(StoreTransfer storeTransfer, int editable):base(editable)
        {
            this.storeTransfer = db.StoreTransfers.Where(c => c.StoreTransferID == storeTransfer.StoreTransferID).Include(p => p.storeFrom).Include(p => p.storeTo).First();
            Information["operationType"] = 3;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);
        }

        override public void CleanCode()
        {
            storeTransfer.Code = "";
            base.CleanCode();
        }

        override public void SetCode(string code)
        {
            storeTransfer.Code = code;
        }

        override public void SetDate(DateTime date)
        {
            storeTransfer.Date = date;
            base.SetDate(date);
        }

        public override void SetStoreFrom(int num)
        {
            storeTransfer.storeFrom = db.Stores.Where(s => s.StoreID == num).First();
            base.SetStore(num);
        }

        public override Store GetStoreFrom()
        {
            return storeTransfer.storeFrom;
        }

        public override Store GetStoreTo()
        {
            return storeTransfer.storeTo;
        }

        public override Shortcuts.ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Traspaso entre Almacenes ({GetCode()})",
                Controller = this

            };
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_STT_Item_Load_StoreTransfer();
                    break;

                case 2:
                    MC_Page = new View.MC_STT_Item_Load_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_STT_Item_Load_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_STT_Item_Load_StoreTransfer();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_STT_Item_Load_StoreTransfer();
        }

        public override string GetCode()
        {
            return storeTransfer.Code;
        }

        public override DateTime GetDate()
        {
            return Convert.ToDateTime(storeTransfer.Date);
        }

        public override int GetDocumentID()
        {
            return storeTransfer.StoreTransferID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("StoreTransfer")).First();
        }

        override public int LastCode()
        {
            if (db.StoreTransfers.ToList().Count > 0)
            {
                lastCode = db.StoreTransfers.OrderBy(u => u.StoreTransferID).Last().StoreTransferID + 1;
                storeTransfer.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                storeTransfer.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_STT_Item_Load_Movements floatWindow = new View.FW_STT_Item_Load_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_STT_Item_Load_Movements floatWindow = new View.FW_STT_Item_Load_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<StoreTransfer> deliveries = db.StoreTransfers.ToList();
            foreach (var item in deliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            storeTransfer.Code = code;

            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if(storeTransfer.Date != null && GetStoreFrom().StoreID > 0 && GetStoreTo().StoreID > 0)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            base.TestMinimalInformation();
        }

        override public void SaveDocument()
        {
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