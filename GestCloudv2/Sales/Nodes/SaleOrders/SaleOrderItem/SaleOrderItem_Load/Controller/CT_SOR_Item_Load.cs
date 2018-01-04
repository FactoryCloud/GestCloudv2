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
    public partial class CT_SOR_Item_Load : Main.Controller.CT_Common
    {
        public StockAdjust stockAdjust;
        public int lastStockAdjustsCod;
        public SaleOrder saleOrder;
        public Movement movementSelected;
        public MovementsView movementsView;
        public ClientsView clientsView;
        public Store store;
        public List<StockAdjust> stocksAdjust;
        public List<Movement> movements;
        public int MovementLastID;

        public CT_SOR_Item_Load(SaleOrder saleOrder, int editable)
        {
            this.saleOrder = db.SaleOrders.Where(c => c.SaleOrderID == saleOrder.SaleOrderID).Include(e => e.client).Include(i => i.client.entity).First();
            clientsView = new ClientsView();
            movementsView = new MovementsView();
            Information.Add("minimalInformation", 0);
            Information.Add("editable",editable);
            Information.Add("old_editable", 0);

            Information["entityValid"] = 1;
            Information["editable"] = editable;

            List<DocumentType> documentTypes = db.DocumentTypes.Where(i => i.Name.Contains("Order")).ToList();

            store = db.Movements.Where(u => u.DocumentID == saleOrder.SaleOrderID&& (documentTypes[0].DocumentTypeID == u.DocumentTypeID || documentTypes[1].DocumentTypeID == u.DocumentTypeID)).Include(u => u.store).First().store;

        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            DocumentType documentType = db.DocumentTypes.Where(i => i.Name.Contains("Order") && i.Input == 0).First();
            movements = db.Movements.Where(u => u.DocumentID == saleOrder.SaleOrderID&& (documentType.DocumentTypeID == u.DocumentTypeID)).Include(u => u.store)
                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList();

            MovementLastID = movements.OrderBy(m => m.MovementID).Last().MovementID;

            foreach (Movement item in movements)
            {
                movementsView.MovementAdd(item);
            }
            UpdateComponents();
        }

        public override void SetSubmenu(int option)
        {
            switch (option)
            {
                case 4:
                    CT_Submenu = new Model.CT_Submenu(store, option);
                    break;

                case 6:
                    CT_Submenu = new Model.CT_Submenu(saleOrder.client, option);
                    break;
            }

            NV_Page = new View.NV_SOR_Item_Load_SaleOrder();
            TopSide.Content = NV_Page;
        }

        public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            List<CompanyStore> companyStores = db.CompaniesStores.Where(c => c.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID).Include(z => z.store).ToList();
            foreach (CompanyStore e in companyStores)
            {
                stores.Add(e.store);
            }
            return stores;
        }

        public void CleanStockCode()
        {
            saleOrder.Code = "";
            TestMinimalInformation();
        }

        public void SetMovementSelected(int num)
        {
            movementSelected = movementsView.movements.Where(u => u.MovementID == num).First();
            if (Information["editable"] == 1)
            {
                if (Information["mode"] == 1)
                    TS_Page = new View.TS_SOR_Item_Load_SaleOrder(Information["minimalInformation"]);

                if (Information["mode"] == 2)
                    TS_Page = new View.TS_SOR_Item_Load_SaleOrder_Movements(Information["minimalInformation"]);

                LeftSide.Content = TS_Page;
            }
        }

        public void SetStore(int num)
        {
            store = db.Stores.Where(s => s.StoreID == num).First();
            TestMinimalInformation();
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {

        }

        public override void EV_UpdateShortcutDocuments(int option)
        {
            base.EV_UpdateShortcutDocuments(option);
            SC_Page = new View.SC_SOR_Item_Load_SaleOrder();
            RightSide.Content = SC_Page;
        }

        public void SetAdjustDate(DateTime date)
        {
            saleOrder.Date = date;
            TestMinimalInformation();
        }


        public int LastStockAdjustCod()
        {
            if (db.StockAdjusts.ToList().Count > 0)
            {
                lastStockAdjustsCod = db.StockAdjusts.OrderBy(u => u.StockAdjustID).Last().StockAdjustID + 1;
                stockAdjust.Code = lastStockAdjustsCod.ToString();
                return lastStockAdjustsCod;
            }
            else
            {
                stockAdjust.Code = $"1";
                return lastStockAdjustsCod = 1;
            }
        }

        public void MD_StoredStock_Reduce()
        {
            View.FW_SOR_Item_Load_ReduceStock floatWindow = new View.FW_SOR_Item_Load_ReduceStock(1, movementsView.movements);
            floatWindow.Show();
        }

        public void MD_StoredStock_Increase()
        {
            View.FW_SOR_Item_Load_IncreaseStock floatWindow = new View.FW_SOR_Item_Load_IncreaseStock(1, movementsView.movements);
            floatWindow.Show();
        }

        public void MD_StoredStock_Remove()
        {
            movementsView.MovementDelete(movementSelected.MovementID);
            movementSelected = null;
            UpdateComponents();
        }

        public void MD_StoredStock_Edit()
        {
            View.FW_SOR_Item_Load_IncreaseStock floatWindow = new View.FW_SOR_Item_Load_IncreaseStock(1, movementsView.movements, movementSelected.MovementID);
            floatWindow.Show();
        }

        public override void EV_MovementAdd(Movement movement)
        {
            //MessageBox.Show(movement.Base.ToString());
            movement.MovementID = movementsView.MovementNextID(MovementLastID);
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
        }

        public Boolean StockAdjustExist(string stocksAdjust)
        {
            List<StockAdjust> stocks = db.StockAdjusts.ToList();
            foreach (var item in stocks)
            {
                if (item.Code.Contains(stocksAdjust) || stocksAdjust.Length == 0)
                {
                    CleanStockCode();
                    return true;
                }
            }
            stockAdjust.Code = stocksAdjust;
            TestMinimalInformation();
            return false;
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if(verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        private void TestMinimalInformation()
        {
            if(saleOrder.Date != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["mode"] == 1)
                TS_Page = new View.TS_SOR_Item_Load_SaleOrder(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_SOR_Item_Load_SaleOrder_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }

        public void SaveNewStockAdjust()
        {
            /*stockAdjust.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            db.StockAdjusts.Add(stockAdjust);
            db.SaveChanges();*/

            foreach (Movement movement in movementsView.movements)
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
                        movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Order" && c.Input == 0).First().DocumentTypeID;
                        movement.StoreID = store.StoreID;
                    }

                    else
                    {
                        movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                        movement.documentType = null;
                    }

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
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_SOR_Item_Load_SaleOrder();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_SOR_Item_Load_SaleOrder(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_SOR_Item_Load_SaleOrder(Information["minimalInformation"]);
                    MC_Page = new View.MC_SOR_Item_Load_SaleOrder();
                    SC_Page = new View.SC_SOR_Item_Load_SaleOrder();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_SOR_Item_Load_SaleOrder();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_SOR_Item_Load_SaleOrder(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_SOR_Item_Load_SaleOrder_Movements(Information["minimalInformation"]);
                    MC_Page = new View.MC_SOR_Item_Load_SaleOrder_Movements();
                    SC_Page = new View.SC_SOR_Item_Load_SaleOrder();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
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

                case 1:
                    /*MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();*/
                    break;
            }
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            TestMinimalInformation();
        }
    }
}