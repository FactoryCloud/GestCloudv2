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

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_DCM_Item_Load.xaml
    /// </summary>
    public partial class CT_DCM_Item_Load : Main.Controller.CT_Common
    {
        public StockAdjust stockAdjust;
        public int lastCode;
        public Movement movementSelected;
        public MovementsView movementsView;
        public ProvidersView providersView;
        public Store store;
        public List<StockAdjust> stocksAdjust;
        public List<Movement> movements;
        public int MovementLastID;

        public CT_DCM_Item_Load(int editable)
        {
            providersView = new ProvidersView();
            movementsView = new MovementsView();
            Information.Add("minimalInformation", 0);
            Information.Add("editable",editable);
            Information.Add("old_editable", 0);

            Information["entityValid"] = 1;
            Information["editable"] = editable;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            MovementLastID = movements.OrderBy(m => m.MovementID).Last().MovementID;

            foreach (Movement item in movements)
            {
                movementsView.MovementAdd(item);
            }
            UpdateComponents();
        }

        public virtual void SetCode(string code)
        {

        }
        public virtual void SetMC(int i)
        {

        }

        public virtual void SetTS()
        {

        }

        public virtual void SetNV()
        {

        }

        public override void SetSubmenu(int option)
        {
            switch (option)
            {
                case 4:
                    CT_Submenu = new Model.CT_Submenu(store, option);
                    break;

                case 7:
                    CT_Submenu = new Model.CT_Submenu(GetProvider(), option);
                    break;
            }

            SetNV();
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

        virtual public Provider GetProvider()
        {
            return new Provider();
        }

        virtual public string GetCode()
        {
            return "0";
        }

        virtual public DateTime GetDate()
        {
            return DateTime.Today;
        }

        virtual public void CleanCode()
        {
            TestMinimalInformation();
        }

        public void SetMovementSelected(int num)
        {
            movementSelected = movementsView.movements.Where(u => u.MovementID == num).First();
            if (Information["editable"] == 1)
            {
                SetNV();
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

        virtual public void SetDate(DateTime date)
        {
            TestMinimalInformation();
        }

        virtual public int LastCode()
        {
            return 1;
        }

        virtual public void MD_MovementAdd()
        {
        }

        public void MD_MovementDelete()
        {
            movementsView.MovementDelete(movementSelected.MovementID);
            movementSelected = null;
            UpdateComponents();
        }

        virtual public void MD_MovementEdit()
        {
        }

        public override void EV_MovementAdd(Movement movement)
        {
            movement.MovementID = movementsView.MovementNextID(MovementLastID);
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
        }

        virtual public Boolean CodeExist(string stocksAdjust)
        {
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

        virtual public void TestMinimalInformation()
        {
            SetTS();
            LeftSide.Content = TS_Page;
        }

        virtual public void SaveDocument()
        {
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
                    SetNV();
                    SetTS();
                    SetMC(1);
                    ChangeComponents();
                    break;

                case 2:
                    SetNV();
                    SetTS();
                    SetMC(2);
                    ChangeComponents();
                    break;
            }
        }

        virtual public void ChangeController()
        {
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            TestMinimalInformation();
        }
    }
}