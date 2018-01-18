using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Documents.DCM_Transfers.Controller
{
    public partial class CT_DCM_Transfers : Main.Controller.CT_Common
    {
        public ItemsView itemsView;

        public PurchaseInvoice purchaseInvoice;
        public SaleInvoice saleInvoice;

        public CT_DCM_Transfers()
        {
            //itemsView = new ItemsView();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetPurchaseInvoice(int num)
        {
            purchaseInvoice = db.PurchaseInvoices.Where(p => p.PurchaseInvoiceID == num).First();
            UpdateComponents();
        }

        public virtual void SetMC()
        {

        }

        public virtual void SetTS()
        {

        }

        public virtual void SetNV()
        {

        }

        public virtual int GetDocumentsCount()
        {
            return 0;
        }

        public virtual string GetInvoiceCode()
        {
            return "";
        }

        public virtual bool InvoiceExist()
        {
            return false;
        }

        public virtual void EV_DocumentAdd()
        {

        }

        public virtual void EV_PurchaseInvoice()
        {

        }

        public virtual Main.Controller.CT_Common SetItemOriginal()
        {
            return new Main.Controller.CT_Common();
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public override void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    SetNV();
                    SetTS();
                    SetMC();
                    ChangeComponents();
                    break;
            }
        }

        public virtual void SetItem(int num)
        {
            
        }

        public virtual void GenerateTransfer()
        {
            db.SaveChanges();
            MessageBox.Show("Transferencia terminada");

            CT_Main();
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = SetItemOriginal();
                    break;            
            }

        }
    }
}

