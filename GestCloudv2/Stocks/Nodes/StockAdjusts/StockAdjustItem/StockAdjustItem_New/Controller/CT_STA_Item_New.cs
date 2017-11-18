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

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_STA_Item_New : Main.Controller.CT_Common
    {
        public StockAdjust stockAdjust;
        public int lastStockAdjustsCod;
        public Movement movementSelected;
        public MovementsView movementsView;
        public Store store;

        public CT_STA_Item_New()
        {
            stockAdjust = new StockAdjust();
            movementsView = new MovementsView();
            Information.Add("minimalInformation", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public List<Company> GetCompanies()
        {
            return db.Companies.ToList();
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

        public void SetMovementSelected(int num)
        {
            movementSelected = movementsView.movements.Where(u => u.MovementID == num).First();
            if (Information["mode"] == 1)
                TS_Page = new View.TS_STA_Item_New_StockAdjust(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_STA_Item_New_StockAdjust_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }

        public void SetStore(int num)
        {
            store = db.Stores.Where(s => s.StoreID == num).First();
            TestMinimalInformation();
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {

        }

        public void SetAdjustDate(DateTime date)
        {
            stockAdjust.Date = date;
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
            View.FW_STA_Item_New_ReduceStock floatWindow = new View.FW_STA_Item_New_ReduceStock(1, movementsView.movements);
            floatWindow.Show();
        }

        public void MD_StoredStock_Increase()
        {
            View.FW_STA_Item_New_IncreaseStock floatWindow = new View.FW_STA_Item_New_IncreaseStock(1, movementsView.movements);
            floatWindow.Show();
        }

        public override void EV_MovementAdd(Movement movement)
        { 
            movement.MovementID = movementsView.MovementNextID();
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
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
            if(stockAdjust.Date != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["mode"] == 1)
                TS_Page = new View.TS_STA_Item_New_StockAdjust(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_STA_Item_New_StockAdjust_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }

        public void SaveNewStockAdjust()
        {
            stockAdjust.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            db.StockAdjusts.Add(stockAdjust);
            db.SaveChanges();

            foreach (Movement movement in movementsView.movements)
            {
                movement.MovementID = 0;
                movement.ConditionID = movement.condition.ConditionID;
                movement.condition = null;
                movement.ProductID = movement.product.ProductID;
                movement.product = null;

                if (movement.store == null)
                    movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "StockAdjust" && c.Input == 1).First().DocumentTypeID; 

                else
                {
                    movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                    movement.documentType = null;
                    if (movement.Quantity < 0)
                        movement.Quantity = movement.Quantity * -1;
                }

                movement.DocumentID = stockAdjust.StockAdjustID;
                db.Movements.Add(movement);
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
                    NV_Page = new View.NV_STA_Item_New_StockAdjust();
                    TS_Page = new View.TS_STA_Item_New_StockAdjust(Information["minimalInformation"]);
                    MC_Page = new View.MC_STA_Item_New_StockAdjust();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_STA_Item_New_StockAdjust();
                    TS_Page = new View.TS_STA_Item_New_StockAdjust_Movements(Information["minimalInformation"]);
                    MC_Page = new View.MC_STA_Item_New_StockAdjust_Movements();
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
                    a.MainFrame.Content = new Stocks.Nodes.StockAdjusts.StockAdjustMenu.Controller.CT_StockAdjustMenu();
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