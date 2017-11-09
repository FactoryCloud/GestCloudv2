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
using Microsoft.SqlServer;
using System.Data;
using System.Collections;

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.View
{
    /// <summary>
    /// Interaction logic for NewUser_MainPage.xaml
    /// </summary>
    public partial class MC_PRO_Item_New : Files.Nodes.Entities.View.MC_Entity_New
    {

        public MC_PRO_Item_New()
        {

        }

        private void ControlFieldsKey_Event(object sender, RoutedEventArgs e)
        {

        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ProviderItem_New.Controller.CT_PRO_Item_New)a.MainFrame.Content;
        }

        /*public Provider provider;
        public int lastProviderCod;
        public MC_PRO_Item_New()
        {
            entity = new Entity();
            provider = new Provider();
            Information.Add("minimalInformation", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveNewProvider()
        {
            if (db.Providers.ToList().Count > 0)
            {
                provider.Cod = db.Providers.OrderBy(u => u.Cod).Last().Cod + 1;
            }
            else
            {
                provider.Cod = 1;
            }

            if (db.Entities.ToList().Count > 0)
            {
                entity.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
            }
            else
            {
                entity.Cod = 1;
            }

            provider.entity = entity;
            db.Entities.Add(entity);
            db.Clients.Add(provider);
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

        override public void MD_EntityLoad()
        {
            View.FW_PRO_Item_Load_Entity floatWindow = new View.FW_PRO_Item_Load_Entity(4);
            floatWindow.Show();
        }

        override public void MD_EntityNew()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3);
        }

        public override void MD_EntityLoaded()
        {
            MD_Change(4);
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

        public Boolean ProviderControlExist(int providerCod)
        {
            List<Provider> providers = db.Providers.ToList();
            foreach (var item in providers)
            {
                if (item.Cod == providerCod)
                {
                    provider.Cod = 0;
                    return true;
                }
            }
            provider.Cod = providerCod;
            TestMinimalInformation();
            return false;
        }

        public int LastProviderCod()
        {
            if (db.Clients.ToList().Count > 0)
            {
                lastProviderCod = db.Providers.OrderBy(u => u.Cod).Last().Cod + 1;
                provider.Cod = lastProviderCod;
                return lastProviderCod;
            }
            else
            {
                provider.Cod = 1;
                return lastProviderCod = 1;

            }
        }

        public void TestMinimalInformation()
        {
            if (provider.Cod > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new View.TS_PRO_Item_New(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
        }


        override public void UpdateComponents()
        {
            if (Information["entityLoaded"] == 1 && Information["mode"] == 2)
                Information["mode"] = 4;

            if (Information["entityLoaded"] == 2 && Information["mode"] == 2)
                Information["mode"] = 3;

            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_New_Client();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new Providertem_New.View.MC_PRO_Item_Load_Entity_Select();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_New_Entity_New();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_Load_Entity_Loaded();
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
                    a.MainFrame.Content = new ProviderMenu.Controller.CT_ProviderMenu();
                    break;
            }
        }*/
    }
}
