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

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_SOR_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public SaleOrder saleOrder;

        public CT_SOR_Item_Load(SaleOrder saleOrder, int editable):base(editable)
        {
            this.saleOrder = db.SaleOrders.Where(c => c.SaleOrderID == saleOrder.SaleOrderID).Include(e => e.client).Include(i => i.client.entity).First();
            Information["operationType"] = 2;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);
        }

        override public void CleanCode()
        {
            saleOrder.Code = "";
            base.CleanCode();
        }

        override public void SetCode(string code)
        {
            saleOrder.Code = code;
        }

        override public void SetDate(DateTime date)
        {
            saleOrder.Date = date;
            base.SetDate(date);
        }

        public override void SetStore(int num)
        {
            saleOrder.store = db.Stores.Where(s => s.StoreID == num).First();
            base.SetStore(num);
        }

        public override Store GetStore()
        {
            return saleOrder.store;
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_SOR_Item_Load_SaleOrder();
                    break;

                case 2:
                    MC_Page = new View.MC_SOR_Item_Load_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_POR_Item_Load_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SOR_Item_Load_PurchaseDelivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SOR_Item_Load_SaleOrder();
        }

        public override string GetCode()
        {
            return saleOrder.Code;
        }

        public override Client GetClient()
        {
            return saleOrder.client;
        }

        public override DateTime GetDate()
        {
            return Convert.ToDateTime(saleOrder.Date);
        }

        public override int GetDocumentID()
        {
            return saleOrder.SaleOrderID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 0 && d.Name.Contains("Order")).First();
        }

        public override Shortcuts.ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Pedido de Venta ({GetCode()})",
                Controller = this

            };
        }

        override public int LastCode()
        {
            if (db.SaleOrders.ToList().Count > 0)
            {
                lastCode = db.SaleOrders.OrderBy(u => u.SaleOrderID).Last().SaleOrderID + 1;
                saleOrder.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                saleOrder.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_SOR_Item_Load_Movements floatWindow = new View.FW_SOR_Item_Load_Movements(Information["operationType"]);
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_SOR_Item_Load_Movements floatWindow = new View.FW_SOR_Item_Load_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<SaleOrder> invoices = db.SaleOrders.ToList();
            foreach (var item in invoices)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            saleOrder.Code = code;

            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if(saleOrder.Date != null && saleOrder.client.ClientID > 0 && GetStore().StoreID > 0)
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

                    movement.DocumentID = saleOrder.SaleOrderID;
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
                    a.MainFrame.Content = new Sales.Nodes.SaleOrders.SaleOrderMenu.Controller.CT_SaleOrderMenu();
                    break;
            }
        }
    }
}