using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_New.Controller
{
    public partial class CT_DCM_Item_New : Main.Controller.CT_Common
    {
        public int lastCode;
        public Movement movementSelected;
        public MovementsView movementsView;
        public List<Movement> movements;
        public Store store;
        public Provider provider;
        public Client client;
        public DocumentContent documentContent;

        public CT_DCM_Item_New()
        {
            provider = new Provider();
            client = new Client();
            movements = new List<Movement>();
            Information.Add("minimalInformation", 0);
            Information.Add("operationType", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            movementsView = new MovementsView(((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, Information["operationType"]);
            movementsView.SetDate(DateTime.Today);
            SetDate(DateTime.Today);
            documentContent = new DocumentContent(Information["operationType"], ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, GetDate(), new List<Movement>());
            UpdateComponents();
            this.Loaded -= EV_PreStart;
        }

        public override void SetSubmenu(int option)
        {
            switch (option)
            {
                case 4:
                    CT_Submenu = new Model.CT_Submenu(store, option);
                    break;

                case 6:
                    CT_Submenu = new Model.CT_Submenu(client, option);
                    break;

                case 7:
                    CT_Submenu = new Model.CT_Submenu(provider, option);
                    break;
            }

            SetNV();
            TopSide.Content = NV_Page;
        }

        public override Store GetStore()
        {
            return store;
        }

        virtual public DateTime GetDate()
        {
           return DateTime.Today;
        }

        virtual public string GetCode()
        {
            return "0";
        }

        virtual public void GetLastCode()
        {

        }

        public int GetMovementNextID()
        {
            if (movements.Count > 0)
            {
                movements.OrderBy(m => m.MovementID);
                return movements.First().MovementID - 1;
            }

            else
                return -1;
        }

        public void SetMovementSelected(int num)
        {
            movementSelected = movementsView.movements.Where(u => u.MovementID == num).First();
            SetTS();
            LeftSide.Content = TS_Page;
        }

        public void SetStore(int num)
        {
            store = db.Stores.Where(s => s.StoreID == num).First();
            TestMinimalInformation();
        }

        virtual public void SetDate(DateTime date)
        {
            movementsView.SetDate(date);
            TestMinimalInformation();
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

        public virtual void SetSC()
        {

        }

        public virtual void SetSB()
        {

        }

        virtual public void CleanCode()
        {
            TestMinimalInformation();
        }

        public void EV_MovementsUpdate()
        {
            documentContent = new DocumentContent(Information["operationType"], ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, GetDate(), movements);
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {

        }

        virtual public void MD_ProviderSelect()
        {

        }

        virtual public void MD_ClientSelect()
        {

        }

        public bool MovementExists(Movement movement)
        {
            if (movements.Where(m => m.MovementID == movement.MovementID).ToList().Count > 0)
                return true;

            else
                return false;
        }

        virtual public void MD_MovementAdd()
        {
        }

        virtual public void MD_MovementEdit()
        {
        }

        public void MD_MovementRemove()
        {
            movements.Remove(movements.Where(m => m.MovementID == movementSelected.MovementID).First());
            movementSelected = null;
            UpdateComponents();
        }

        public override void EV_SetProvider(int num)
        {
            provider = db.Providers.Where(p => p.ProviderID == num).Include(e => e.entity).First();
            EV_UpdateSubMenu(7);
            SetMC(1);
            MainContent.Content = MC_Page;
        }

        public override void EV_SetClient(int num)
        {
            client = db.Clients.Where(p => p.ClientID == num).Include(e => e.entity).First();
            EV_UpdateSubMenu(6);
            SetMC(1);
            MainContent.Content = MC_Page;
        }

        public override void EV_MovementAdd(Movement movement)
        {
            if (!MovementExists(movement))
            {
                movement.MovementID = GetMovementNextID();
                movements.Add(movement);
            }

            else
            {
                movements.Remove(movements.Where(m => m.MovementID == movement.MovementID).First());
                movements.Add(movement);
                movements.OrderBy(m => m.MovementID);
            }

            movementSelected = null;
            EV_MovementsUpdate();
            UpdateComponents();
        }

        public virtual Boolean CodeExist(string test)
        {
            return false;
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if (verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        public virtual void TestMinimalInformation()
        {
            SetTS();
            LeftSide.Content = TS_Page;
        }

        public virtual void SaveDocument()
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
                    SetSC();
                    ChangeComponents();
                    break;

                case 2:
                    SetNV();
                    SetTS();
                    SetMC(2);
                    SetSC();
                    ChangeComponents();
                    break;

                case 3:
                    SetNV();
                    SetTS();
                    SetMC(3);
                    SetSC();
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
