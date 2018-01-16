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

        public CT_DCM_Transfers()
        {
            itemsView = new ItemsView();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        virtual public void SetItem(int num)
        {
            SetTS();
            LeftSide.Content = TS_Page;
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

        public virtual Main.Controller.CT_Common SetItemOriginal()
        {
            return new Main.Controller.CT_Common();
        }

        public virtual DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New();
        }

        public virtual DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load(0);
        }

        public virtual DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load(1);
        }

        virtual public bool SelectedItem()
        {
            return false;
        }

        public void EV_CT_New()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void EV_CT_Load()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void EV_CT_LoadEditable()
        {
            Information["controller"] = 3;
            ChangeController();
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
                    SetMC(1);
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = SetItemOriginal();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = SetItemNew();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = SetItemLoad();
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = SetItemLoadEditable();
                    break;
            
            }

        }
    }
}

