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

namespace GestCloudv2.Sales.Nodes.SaleInvoices.SaleInvoiceItem.SaleInvoiceItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_SIN_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public SaleInvoice saleInvoice;

        public CT_SIN_Item_Load(SaleInvoice saleInvoice, int editable):base(editable)
        {
            this.saleInvoice = db.SaleInvoices.Where(c => c.SaleInvoiceID == saleInvoice.SaleInvoiceID).Include(e => e.client).Include(i => i.client.entity).Include(s => s.store).Include(p => p.paymentMethod).First();
            Information["operationType"] = 2;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);
        }

        override public void CleanCode()
        {
            saleInvoice.Code = "";
            base.CleanCode();
        }

        override public void SetCode(string code)
        {
            saleInvoice.Code = code;
        }

        override public void SetDate(DateTime date)
        {
            saleInvoice.Date = date;
            base.SetDate(date);
        }

        public override void SetStore(int num)
        {
            saleInvoice.store = db.Stores.Where(s => s.StoreID == num).First();
            base.SetStore(num);
        }

        public override void SetPaymentMethod(int num)
        {
            saleInvoice.paymentMethod = db.PaymentMethods.Where(s => s.PaymentMethodID == num).First();
            base.SetPaymentMethod(num);
        }

        public override Store GetStore()
        {
            return saleInvoice.store;
        }

        public override PaymentMethod GetPaymentMethod()
        {
            return saleInvoice.paymentMethod;
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_SIN_Item_Load_SaleInvoice();
                    break;

                case 2:
                    MC_Page = new View.MC_SIN_Item_Load_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_SIN_Item_Load_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SIN_Item_Load_PurchaseDelivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SIN_Item_Load_PurchaseDelivery();
        }

        public override Shortcuts.ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Factura de Venta ({GetCode()})",
                Controller = this

            };
        }

        public override string GetCode()
        {
            return saleInvoice.Code;
        }

        public override Client GetClient()
        {
            return saleInvoice.client;
        }

        public override DateTime GetDate()
        {
            return Convert.ToDateTime(saleInvoice.Date);
        }

        public override int GetDocumentID()
        {
            return saleInvoice.SaleInvoiceID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 0 && d.Name.Contains("Invoice")).First();
        }

        override public int LastCode()
        {
            if (db.SaleInvoices.ToList().Count > 0)
            {
                lastCode = db.SaleInvoices.OrderBy(u => u.SaleInvoiceID).Last().SaleInvoiceID + 1;
                saleInvoice.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                saleInvoice.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_SIN_Item_Load_Movements floatWindow = new View.FW_SIN_Item_Load_Movements();
            switch (((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).GetConfigValue(db.Configurations.Where(c => c.Name.Contains("LimiteStock")).First().ConfigurationID))
            {
                case 0:
                    floatWindow = new View.FW_SIN_Item_Load_Movements();
                    break;

                case 1:
                    floatWindow = new View.FW_SIN_Item_Load_Movements(Information["operationType"], movements);
                    break;
            }
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_SIN_Item_Load_Movements floatWindow = new View.FW_SIN_Item_Load_Movements();
            switch (((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).GetConfigValue(db.Configurations.Where(c => c.Name.Contains("LimiteStock")).First().ConfigurationID))
            {
                case 0:
                    floatWindow = new View.FW_SIN_Item_Load_Movements(new Movement(movementSelected));
                    break;

                case 1:
                    floatWindow = new View.FW_SIN_Item_Load_Movements(Information["operationType"], movements, new Movement(movementSelected));
                    break;
            }
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<SaleInvoice> invoices = db.SaleInvoices.ToList();
            foreach (var item in invoices)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            saleInvoice.Code = code;

            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if(saleInvoice.Date != null && saleInvoice.client.ClientID > 0 && GetStore().StoreID > 0)
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
            /*foreach (Movement movement in movementsView.movements)
            {
                if (!movements.Contains(movement))
                {
                    movement.MovementID = 0;
                    movement.ConditionID = movement.condition.ConditionID;
                    movement.condition = null;
                    movement.ProductID = movement.product.ProductID;
                    movement.product = null;
                    movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Invoice" && c.Input == 0).First().DocumentTypeID;
                    movement.StoreID = GetStore().StoreID;

                    movement.DocumentID = saleInvoice.SaleInvoiceID;
                    db.Movements.Add(movement);
                }

                else
                {
                    Movement temp = db.Movements.Where(m => m.MovementID == movement.MovementID).First();
                    temp.ConditionID = movement.condition.ConditionID;
                    temp.ProductID = movement.product.ProductID;
                    temp.Quantity = movement.Quantity;
                    temp.PurchasePrice = movement.PurchasePrice;
                    temp.SalePrice = movement.SalePrice;
                    temp.IsAltered = movement.IsAltered;
                    temp.IsFoil = movement.IsFoil;
                    temp.IsPlayset = movement.IsPlayset;
                    temp.IsSigned = movement.IsSigned;
                    db.Movements.Update(temp);
                }
            }

            foreach (Movement mov in movements)
            {
                if (!movementsView.movements.Contains(mov))
                    db.Movements.Remove(mov);
            }*/

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
                    a.MainFrame.Content = new Sales.Nodes.SaleInvoices.SaleInvoiceMenu.Controller.CT_SaleInvoiceMenu();
                    break;
            }
        }
    }
}