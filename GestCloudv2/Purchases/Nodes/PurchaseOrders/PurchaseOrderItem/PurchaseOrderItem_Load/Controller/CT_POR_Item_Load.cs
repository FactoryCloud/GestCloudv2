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

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderItem.PurchaseOrderItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_POR_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public PurchaseOrder purchaseOrder;

        public CT_POR_Item_Load(PurchaseOrder purchaseOrder, int editable):base(editable)
        {
            this.purchaseOrder = db.PurchaseOrders.Where(c => c.PurchaseOrderID == purchaseOrder.PurchaseOrderID).Include(e => e.provider).Include(i => i.provider.entity).First();
            List<DocumentType> documentTypes = db.DocumentTypes.Where(i => i.Name.Contains("Order")).ToList();
            store = db.Movements.Where(u => u.DocumentID == purchaseOrder.PurchaseOrderID && (documentTypes[0].DocumentTypeID == u.DocumentTypeID || documentTypes[1].DocumentTypeID == u.DocumentTypeID)).Include(u => u.store).First().store;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            DocumentType documentType = db.DocumentTypes.Where(i => i.Name.Contains("Order") && i.Input == 1).First();
            movements = db.Movements.Where(u => u.DocumentID == purchaseOrder.PurchaseOrderID && (documentType.DocumentTypeID == u.DocumentTypeID)).Include(u => u.store)
                .Include(i => i.product).Include(z => z.condition).Include(i => i.product.productType).ToList();

            MovementLastID = movements.OrderBy(m => m.MovementID).Last().MovementID;

            foreach (Movement item in movements)
            {
                movementsView.MovementAdd(item);
            }
            UpdateComponents();
        }

        public override void SetMC(int i)
        {
            switch(i)
            {
                case 1:
                    MC_Page = new View.MC_POR_Item_Load_PurchaseOrder();
                    break;

                case 2:
                    MC_Page = new View.MC_POR_Item_Load_Movements();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_POR_Item_Load_PurchaseOrder(Information["minimalInformation"]);
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_POR_Item_Load_PurchaseOrder();
        }

        public override Provider GetProvider()
        {
            return purchaseOrder.provider;
        }

        public override string GetCode()
        {
            return $"{purchaseOrder.Code}";
        }

        public override DateTime GetDate()
        {
            return (DateTime)purchaseOrder.Date;
        }

        override public void CleanCode()
        {
            purchaseOrder.Code = "";
            base.CleanCode();
        }

        override public void SetDate(DateTime date)
        {
            purchaseOrder.Date = date;
            base.SetDate(date);
        }


        override public int LastCode()
        {
            if (db.StockAdjusts.ToList().Count > 0)
            {
                lastCode = db.StockAdjusts.OrderBy(u => u.StockAdjustID).Last().StockAdjustID + 1;
                stockAdjust.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                stockAdjust.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_POR_Item_Load_IncreaseStock floatWindow = new View.FW_POR_Item_Load_IncreaseStock(1, movementsView.movements);
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_POR_Item_Load_IncreaseStock floatWindow = new View.FW_POR_Item_Load_IncreaseStock(1, movementsView.movements, movementSelected.MovementID);
            floatWindow.Show();
        }

        override public Boolean CodeExist(string order)
        {
            List<PurchaseOrder> orders = db.PurchaseOrders.ToList();
            foreach (var item in orders)
            {
                if (item.Code.Contains(order) || order.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            purchaseOrder.Code = order;
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
            if(purchaseOrder.Date != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            SetTS();
            LeftSide.Content = TS_Page;
        }

        public void SaveNewStockAdjust()
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
                        movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Order" && c.Input == 1).First().DocumentTypeID;
                        movement.StoreID = store.StoreID;
                    }

                    else
                    {
                        movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                        movement.documentType = null;
                    }

                    movement.DocumentID = purchaseOrder.PurchaseOrderID;
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
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.Controller.CT_PurchaseOrderMenu();
                    break;
            }
        }
    }
}