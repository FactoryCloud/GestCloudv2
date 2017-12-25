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
using System.Security.Cryptography;

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_New.xaml
    /// </summary>
    public partial class CT_PTY_Item_New : Main.Controller.CT_Common
    {
        public int lastProductType;
        public ProductType productType;
        public SubmenuItems submenuItems;

        public CT_PTY_Item_New()
        {
            submenuItems = new SubmenuItems();
            productType = new ProductType();
            Information.Add("minimalInformation", 0);
            Information["entityValid"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetProductTypeName(string name)
        {
            productType.Name = name;
            TestMinimalInformation();
        }

        public void SetProductTypeCode(int code)
        {
            productType.Code = code;
            TestMinimalInformation();
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

        public void CleanName()
        {
            productType.Name = "";
            TestMinimalInformation();
        }

        private void TestMinimalInformation()
        {
            if(productType.Name.Length > 0 && productType.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new View.TS_PTY_Item_New(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
        }

        public void SaveNewProductType()
        {
            db.ProductTypes.Add(productType);
            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        public int LastProductType()
        {
            if (db.ProductTypes.ToList().Count > 0)
            {
                lastProductType = db.ProductTypes.OrderBy(u => u.Code).Last().Code + 1;
                productType.Code = lastProductType;
                return lastProductType;
            }
            else
            {
                productType.Code = 1;
                return lastProductType = 1;

            }
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
                    NV_Page = new View.NV_PTY_Item_New();
                    TS_Page = new View.TS_PTY_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_PTY_Item_New_ProductType();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PTY_Item_New();
                    TS_Page = new View.TS_PTY_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_PTY_Item_New_ProductType_Taxes();
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
                    a.MainFrame.Content = new ProductTypeMenu.Controller.CT_ProductTypeMenu();
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