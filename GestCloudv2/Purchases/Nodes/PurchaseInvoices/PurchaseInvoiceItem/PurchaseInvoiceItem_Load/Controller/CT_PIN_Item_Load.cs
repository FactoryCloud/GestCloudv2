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

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceItem.PurchaseInvoiceItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_PIN_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public PurchaseInvoice purchaseInvoice;

        public CT_PIN_Item_Load(PurchaseInvoice purchaseInvoice, int editable):base(editable)
        {
            this.purchaseInvoice = db.PurchaseInvoices.Where(c => c.PurchaseInvoiceID == purchaseInvoice.PurchaseInvoiceID).Include(e => e.provider).Include(i => i.provider.entity).Include(p => p.store).First();
            Information["operationType"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);
        }

        override public void CleanCode()
        {
            purchaseInvoice.Code = "";
            base.CleanCode();
        }

        override public void SetCode(string code)
        {
            purchaseInvoice.Code = code;
        }

        override public void SetDate(DateTime date)
        {
            purchaseInvoice.Date = date;
            base.SetDate(date);
        }

        public override void SetStore(int num)
        {
            purchaseInvoice.store = db.Stores.Where(s => s.StoreID == num).First();
            base.SetStore(num);
        }

        public override Store GetStore()
        {
            return purchaseInvoice.store;
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_PIN_Item_Load_PurchaseInvoice();
                    break;

                case 2:
                    MC_Page = new View.MC_PIN_Item_Load_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_PIN_Item_Load_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PIN_Item_Load_PurchaseInvoice();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PIN_Item_Load_PurchaseInvoice();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_PIN_Item_Load_PurchaseOrder();
        }

        public override string GetCode()
        {
            return purchaseInvoice.Code;
        }

        public override Provider GetProvider()
        {
            return purchaseInvoice.provider;
        }

        public override DateTime GetDate()
        {
            return Convert.ToDateTime(purchaseInvoice.Date);
        }

        public override int GetDocumentID()
        {
            return purchaseInvoice.PurchaseInvoiceID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("Invoice")).First();
        }

        override public int LastCode()
        {
            if (db.PurchaseDeliveries.ToList().Count > 0)
            {
                lastCode = db.PurchaseDeliveries.OrderBy(u => u.PurchaseDeliveryID).Last().PurchaseDeliveryID + 1;
                purchaseInvoice.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                purchaseInvoice.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_PIN_Item_Load_Movements floatWindow = new View.FW_PIN_Item_Load_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_PIN_Item_Load_Movements floatWindow = new View.FW_PIN_Item_Load_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<PurchaseDelivery> deliveries = db.PurchaseDeliveries.ToList();
            foreach (var item in deliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            purchaseInvoice.Code = code;

            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if(purchaseInvoice.Date != null && purchaseInvoice.provider.ProviderID > 0 && GetStore().StoreID > 0)
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

                    if (movement.store == null)
                    {
                        movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Invoice" && c.Input == 1).First().DocumentTypeID;
                        movement.StoreID = GetStore().StoreID;
                    }

                    else
                    {
                        movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                        movement.documentType = null;
                    }

                    movement.DocumentID = purchaseInvoice.PurchaseInvoiceID;
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
                    a.MainFrame.Content = new PurchaseInvoices.PurchaseInvoiceMenu.Controller.CT_PurchaseInvoiceMenu();
                    break;
            }
        }
    }
}