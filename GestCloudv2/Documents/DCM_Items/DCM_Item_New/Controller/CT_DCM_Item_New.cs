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
        public List<Movement> movements;
        public Store store;
        public PaymentMethod paymentMethod;
        public Provider provider;
        public Client client;
        public DocumentContent documentContent;

        public CT_DCM_Item_New()
        {
            movements = new List<Movement>();
            Information.Add("minimalInformation", 0);
            Information.Add("operationType", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            documentContent = new DocumentContent(Information["operationType"], ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, GetDate(), new List<Movement>(), GetClient());
            UpdateComponents();
            this.Loaded -= EV_PreStart;
            this.Loaded += new RoutedEventHandler(EV_ReStart);
        }

        public override void EV_ReStart(object sender, RoutedEventArgs e)
        {
            db.Dispose();
            db = new GestCloudDB();
            SetSC();
            UpdateComponents();
        }

        public virtual DocumentType GetDocumentType()
        {
            return new DocumentType();
        }

        public virtual int GetDocumentID()
        {
            return 0;
        }

        virtual public DateTime GetDate()
        {
           return DateTime.Today;
        }

        virtual public string GetCode()
        {
            return "0";
        }

        public override Store GetStore()
        {
            return store;
        }

        virtual public Store GetStoreFrom()
        {
            return new Store();
        }

        virtual public Store GetStoreTo()
        {
            return new Store();
        }

        public override Provider GetProvider()
        {
            return provider;
        }

        public override Client GetClient()
        {
            return client;
        }

        virtual public void GetLastCode()
        {

        }

        public int GetMovementNextID()
        {
            if (movements.Count > 0)
            {
                movements = movements.OrderBy(m => m.MovementID).ToList();
                return movements.First().MovementID - 1;
            }

            else
                return -1;
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_DCM_Item_New_Main();
        }

        public void SetMovementSelected(int num)
        {
            movementSelected = movements.Where(u => u.MovementID == num).First();
            SetTS();
            LeftSide.Content = TS_Page;
        }

        public void SetStore(int num)
        {
            store = db.Stores.Where(s => s.StoreID == num).First();
            TestMinimalInformation();
        }

        public virtual void SetStoreFrom(int num)
        {
            TestMinimalInformation();
        }

        public virtual void SetStoreTo(int num)
        {
            TestMinimalInformation();
        }

        public void SetPaymentMethod(int num)
        {
            paymentMethod = db.PaymentMethods.Where(p => p.PaymentMethodID == num).First();
            TestMinimalInformation();
        }

        virtual public void SetDate(DateTime date)
        {
            documentContent.SetDate(date);
            TestMinimalInformation();
        }        

        virtual public void CleanCode()
        {
            TestMinimalInformation();
        }

        public void EV_MovementsUpdate()
        {
            documentContent = new DocumentContent(Information["operationType"], ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, GetDate(), movements, GetClient());
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
            EV_MovementsUpdate();
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
                MD_MovementAdd();
            }

            else
            {
                movements.Remove(movements.Where(m => m.MovementID == movement.MovementID).First());
                movements.Add(movement);
                movements.OrderBy(m => m.MovementID);
            }

            EV_MovementsUpdate();
            movementSelected = null;
            
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

        public void TestMinimalInformation()
        {
            switch(Information["operationType"])
            {
                case 1:
                    if (GetDate() != null && GetProvider() != null && GetStore() != null)
                        Information["minimalInformation"] = 1;

                    else
                        Information["minimalInformation"] = 0;

                    break;

                case 2:
                    if (GetDate() != null && GetClient() != null && GetStore() != null)
                        Information["minimalInformation"] = 1;

                    else
                        Information["minimalInformation"] = 0;

                    break;
            }

            SetTS();
            LeftSide.Content = TS_Page;
        }

        

        public virtual void SaveDocument()
        {
            foreach (Movement mov in movements)
            {
                db.Movements.Add(new Movement
                {
                    ProductID = mov.ProductID,
                    StoreID = mov.StoreID,
                    DocumentID = GetDocumentID(),
                    DocumentTypeID = GetDocumentType().DocumentTypeID,
                    Quantity = Convert.ToDecimal(mov.Quantity),
                    PurchasePrice = Convert.ToDecimal(mov.PurchasePrice),
                    SalePrice = Convert.ToDecimal(mov.SalePrice),
                    PurchaseDiscount1 = Convert.ToDecimal(mov.PurchaseDiscount1),
                    SaleDiscount1 = Convert.ToDecimal(mov.SaleDiscount1),
                });
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
