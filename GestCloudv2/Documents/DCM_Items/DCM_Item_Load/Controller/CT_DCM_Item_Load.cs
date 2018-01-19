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
        public int lastCode;
        public Movement movementSelected;
        public List<Movement> movements;
        public List<Movement> movementsOld;
        public List<Movement> movementsTransfer;
        public DocumentContent documentContent;

        public CT_DCM_Item_Load(int editable)
        {
            movements = new List<Movement>();
            movementsOld = new List<Movement>();
            movementsTransfer = new List<Movement>();
            Information.Add("minimalInformation", 0);
            Information.Add("editable",editable);
            Information.Add("old_editable", 0);
            Information.Add("operationType", 0);

            Information["entityValid"] = 1;
            Information["editable"] = editable;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            movementsOld = db.Movements.Where(u => u.DocumentID == GetDocumentID() && (GetDocumentType().DocumentTypeID == u.DocumentTypeID)).Include(u => u.store)
                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList();

            switch(GetDocumentType().Name)
            {
                case "Invoice":
                    DocumentType docType1 = db.DocumentTypes.Where(d => d.Name.Contains("Delivery") && d.Input == GetDocumentType().Input).First();
                    DocumentType docType2 = db.DocumentTypes.Where(d => d.Name.Contains("Order") && d.Input == GetDocumentType().Input).First();
                    if (GetDocumentType().Input == 1)
                    {
                        List<PurchaseDelivery> deliveries = db.PurchaseDeliveries.Where(p => p.PurchaseInvoiceID == GetDocumentID()).ToList();
                        foreach(PurchaseDelivery item in deliveries)
                        {
                            movementsTransfer.AddRange(db.Movements.Where(p => p.DocumentTypeID == docType1.DocumentTypeID && p.DocumentID == item.PurchaseDeliveryID).Include(u => u.store)
                                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList());
                        }

                        List<PurchaseOrder> orders = db.PurchaseOrders.Where(p => p.PurchaseInvoiceID == GetDocumentID()).ToList();
                        foreach (PurchaseOrder item in orders)
                        {
                            movementsTransfer.AddRange(db.Movements.Where(p => p.DocumentTypeID == docType2.DocumentTypeID && p.DocumentID == item.PurchaseOrderID).Include(u => u.store)
                                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList());
                        }
                    }

                    if (GetDocumentType().Input == 0)
                    {
                        List<SaleDelivery> deliveries = db.SaleDeliveries.Where(p => p.SaleInvoiceID == GetDocumentID()).ToList();
                        foreach (SaleDelivery item in deliveries)
                        {
                            movementsTransfer.AddRange(db.Movements.Where(p => p.DocumentTypeID == docType1.DocumentTypeID && p.DocumentID == item.SaleDeliveryID).Include(u => u.store)
                                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList());
                        }

                        List<SaleOrder> orders = db.SaleOrders.Where(p => p.SaleInvoiceID == GetDocumentID()).ToList();
                        foreach (SaleOrder item in orders)
                        {
                            movementsTransfer.AddRange(db.Movements.Where(p => p.DocumentTypeID == docType2.DocumentTypeID && p.DocumentID == item.SaleOrderID).Include(u => u.store)
                                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList());
                        }
                    }
                    break;

                case "Delivery":
                    DocumentType docType = db.DocumentTypes.Where(d => d.Name.Contains("Order") && d.Input == GetDocumentType().Input).First();
                    if (GetDocumentType().Input == 1)
                    {
                        List<PurchaseOrder> orders = db.PurchaseOrders.Where(p => p.PurchaseDeliveryID == GetDocumentID()).ToList();
                        foreach (PurchaseOrder item in orders)
                        {
                            movementsTransfer.AddRange(db.Movements.Where(p => p.DocumentTypeID == docType.DocumentTypeID && p.DocumentID == item.PurchaseOrderID).Include(u => u.store)
                                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList());
                        }
                    }

                    if (GetDocumentType().Input == 0)
                    {
                        List<SaleOrder> orders = db.SaleOrders.Where(p => p.SaleDeliveryID == GetDocumentID()).ToList();
                        foreach (SaleOrder item in orders)
                        {
                            movementsTransfer.AddRange(db.Movements.Where(p => p.DocumentTypeID == docType.DocumentTypeID && p.DocumentID == item.SaleOrderID).Include(u => u.store)
                                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList());
                        }
                    }
                    break;
            }

            List<Movement> allMovements = new List<Movement>();
            allMovements.AddRange(movementsOld);
            allMovements.AddRange(movementsTransfer);
            documentContent = new DocumentContent(Information["operationType"],((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, GetDate(), allMovements);
            UpdateComponents();
            this.Loaded -= EV_PreStart;
        }

        public void SetMovementSelected(int num)
        {
            if(num > 0)
                movementSelected = movementsOld.Where(m => m.MovementID == num).First();
            
            else
                movementSelected = movements.Where(m => m.MovementID == num).First();

            if (Information["editable"] == 1)
            {
                SetTS();
                LeftSide.Content = TS_Page;
            }
        }

        public virtual void SetStore(int num)
        {
            TestMinimalInformation();
        }

        virtual public void SetDate(DateTime date)
        {
            documentContent.SetDate(date);
            TestMinimalInformation();
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

        public virtual void SetSC()
        {

        }

        public override void SetSubmenu(int option)
        {
            switch (option)
            {
                case 4:
                    CT_Submenu = new Model.CT_Submenu(GetStore(), option);
                    break;

                case 6:
                    CT_Submenu = new Model.CT_Submenu(GetClient(), option);
                    break;

                case 7:
                    CT_Submenu = new Model.CT_Submenu(GetProvider(), option);
                    break;
            }

            SetNV();
            TopSide.Content = NV_Page;
        }

        virtual public Provider GetProvider()
        {
            return new Provider();
        }

        virtual public Client GetClient()
        {
            return new Client();
        }

        virtual public string GetCode()
        {
            return "0";
        }

        override public Store GetStore()
        {
            return new Store();
        }

        virtual public DateTime GetDate()
        {
            return DateTime.Today;
        }

        virtual public int GetDocumentID()
        {
            return 0;
        }

        virtual public DocumentType GetDocumentType()
        {
            return new DocumentType();
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

        virtual public int LastCode()
        {
            return 1;
        }

        virtual public void CleanCode()
        {
            TestMinimalInformation();
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {

        }

        public bool MovementExists(Movement movement)
        {
            if (movements.Where(m => m.MovementID == movement.MovementID).ToList().Count > 0 || movementsOld.Where(m => m.MovementID == movement.MovementID).ToList().Count > 0)
                return true;

            else
                return false;
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
                if (movements.Where(m => m.MovementID == movement.MovementID).ToList().Count > 0)
                {
                    movements.Remove(movements.Where(m => m.MovementID == movement.MovementID).First());
                    movements.Add(movement);
                    movements.OrderBy(m => m.MovementID);
                }

                if (movementsOld.Where(m => m.MovementID == movement.MovementID).ToList().Count > 0)
                {
                    movementsOld.Remove(movementsOld.Where(m => m.MovementID == movement.MovementID).First());
                    movementsOld.Add(movement);
                    movementsOld.OrderBy(m => m.MovementID);
                }
            }

            movementSelected = null;

            EV_MovementsUpdate();
            UpdateComponents();
        }

        public void EV_MovementsUpdate()
        {
            List<Movement> allMovements = new List<Movement>();
            allMovements.AddRange(movementsOld);
            allMovements.AddRange(movementsTransfer);
            allMovements.AddRange(movements);
            documentContent = new DocumentContent(Information["operationType"], ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany, GetDate(), allMovements);
        }

        virtual public void MD_MovementAdd()
        {
        }

        public void MD_MovementDelete()
        {
            if(movementSelected.MovementID > 0)
                movementsOld.Remove(movementsOld.Where(m => m.MovementID == movementSelected.MovementID).First());

            else
                movements.Remove(movements.Where(m => m.MovementID == movementSelected.MovementID).First());

            movementSelected = null;
            EV_MovementsUpdate();
            UpdateComponents();
        }

        virtual public void MD_MovementEdit()
        {
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
            db.Dispose();

            db = new GestCloudDB();

            List<Movement> movementsTemp = db.Movements.Where(u => u.DocumentID == GetDocumentID() && (GetDocumentType().DocumentTypeID == u.DocumentTypeID)).Include(u => u.store)
                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList();

            List<Movement> movementsEdit = new List<Movement>();

            foreach (Movement mov in movementsTemp)
            {
                if (movementsOld.Where(m => m.MovementID == mov.MovementID).ToList().Count == 0)
                    db.Movements.Remove(db.Movements.Where(m => m.MovementID == mov.MovementID).First());

                else
                {
                    Movement temp = movementsOld.Where(m => m.MovementID == mov.MovementID).First();
                    if (temp.ProductID != mov.ProductID || temp.Quantity != mov.Quantity || temp.PurchasePrice != mov.PurchasePrice || temp.SalePrice != mov.SalePrice
                        || temp.StoreID != mov.StoreID || temp.ConditionID != mov.ConditionID)
                    {
                        movementsEdit.Add(temp);
                    }
                }
            }

            foreach(Movement mov in movements)
            {
                db.Movements.Add(new Movement
                {
                    ProductID = mov.ProductID,
                    StoreID = GetStore().StoreID,
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

            db.Dispose();

            db = new GestCloudDB();

            foreach(Movement item in movementsEdit)
            {
                Movement final = db.Movements.Where(m => m.MovementID == item.MovementID).First();
                final.ProductID = item.ProductID;
                final.ConditionID = item.ConditionID;
                final.StoreID = item.StoreID;
                final.Quantity = item.Quantity;
                final.PurchasePrice = item.PurchasePrice;
                final.SalePrice = item.Quantity;
                final.PurchaseDiscount1 = item.PurchaseDiscount1;
                final.SaleDiscount1 = item.SaleDiscount1;
                db.Movements.Update(final);
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