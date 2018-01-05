﻿using System;
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

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_PDE_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public PurchaseDelivery purchaseDelivery;

        public CT_PDE_Item_Load(PurchaseDelivery purchaseDelivery, int editable):base(editable)
        {
            this.purchaseDelivery = db.PurchaseDeliveries.Where(c => c.PurchaseDeliveryID == purchaseDelivery.PurchaseDeliveryID).Include(e => e.provider).Include(i => i.provider.entity).First();
            List<DocumentType> documentTypes = db.DocumentTypes.Where(i => i.Name.Contains("Delivery")).ToList();
            store = db.Movements.Where(u => u.DocumentID == purchaseDelivery.PurchaseDeliveryID && (documentTypes[0].DocumentTypeID == u.DocumentTypeID || documentTypes[1].DocumentTypeID == u.DocumentTypeID)).Include(u => u.store).First().store;
            Information["operationType"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            DocumentType documentType = db.DocumentTypes.Where(i => i.Name.Contains("Delivery") && i.Input == 1).First();
            movements = db.Movements.Where(u => u.DocumentID == purchaseDelivery.PurchaseDeliveryID && (documentType.DocumentTypeID == u.DocumentTypeID)).Include(u => u.store)
                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList();

            base.EV_Start(sender, e);
        }

        override public void CleanCode()
        {
            purchaseDelivery.Code = "";
            base.CleanCode();
        }

        override public void SetCode(string code)
        {
            purchaseDelivery.Code = code;
        }

        override public void SetDate(DateTime date)
        {
            purchaseDelivery.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_PDE_Item_Load_PurchaseDelivery();
                    break;

                case 2:
                    MC_Page = new View.MC_PDE_Item_Load_Movements();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PDE_Item_Load_PurchaseDelivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PDE_Item_Load_PurchaseDelivery();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_PDE_Item_Load_PurchaseOrder();
        }

        public override string GetCode()
        {
            return purchaseDelivery.Code;
        }

        public override Provider GetProvider()
        {
            return purchaseDelivery.provider;
        }

        public override DateTime GetDate()
        {
            return Convert.ToDateTime(purchaseDelivery.Date);
        }

        override public int LastCode()
        {
            if (db.PurchaseDeliveries.ToList().Count > 0)
            {
                lastCode = db.PurchaseDeliveries.OrderBy(u => u.PurchaseDeliveryID).Last().PurchaseDeliveryID + 1;
                purchaseDelivery.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                purchaseDelivery.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_PDE_Item_Load_Movements floatWindow = new View.FW_PDE_Item_Load_Movements(1, movementsView.movements);
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_PDE_Item_Load_Movements floatWindow = new View.FW_PDE_Item_Load_Movements(1, movementsView.movements, movementSelected.MovementID);
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
            purchaseDelivery.Code = code;

            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if(purchaseDelivery.Date != null && purchaseDelivery.provider.ProviderID > 0 && store.StoreID > 0)
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
                        movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Delivery" && c.Input == 1).First().DocumentTypeID;
                        movement.StoreID = store.StoreID;
                    }

                    else
                    {
                        movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                        movement.documentType = null;
                    }

                    movement.DocumentID = purchaseDelivery.PurchaseDeliveryID;
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
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.Controller.CT_PurchaseDeliveryMenu();
                    break;
            }
        }
    }
}