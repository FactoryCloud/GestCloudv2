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
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeMenu.Controller
{
    /// <summary>
    /// Interaction logic for CT_StoreMenu.xaml
    /// </summary>
    public partial class CT_ProductTypeMenu : Main.Controller.CT_Common
    {
        public ProductTypesView productTypesView;
        public ProductType productyType;

        public CT_ProductTypeMenu()
        {
            productTypesView = new ProductTypesView();
        }

        override public void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void CT_ProductTypeNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void EV_CT_ProductTypeLoad()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void EV_CT_ProductTypeLoadEditable()
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
            switch(Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new Files.Nodes.ProductTypes.ProductTypeMenu.View.NV_PTY_Menu();
                    TS_Page = new Files.Nodes.ProductTypes.ProductTypeMenu.View.TS_PTY_Menu(); ;
                    MC_Page = new Files.Nodes.ProductTypes.ProductTypeMenu.View.MC_PTY_Menu(); ;
                    ChangeComponents();
                    break;

                case 2:
                    ChangeComponents();
                    break;

                case 3:
                    ChangeComponents();
                    break;

                case 4:
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
                    a.MainFrame.Content = new Files.Controller.CT_Files();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new ProductTypeItem.ProductTypeItem_New.Controller.CT_PTY_Item_New();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new ProductTypeItem.ProductTypeItem_Load.Controller.CT_PTY_Item_Load(productyType, 0);
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new ProductTypeItem.ProductTypeItem_Load.Controller.CT_PTY_Item_Load(productyType, 1);
                    break;
            }
        }
    }
}
