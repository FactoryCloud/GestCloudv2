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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_PRO_Item_New.xaml
    /// </summary>
    public partial class CT_PRO_Item_New : Main.Controller.CT_Common
    {
        public Provider provider;

        public CT_PRO_Item_New()
        {
            entity = new Entity();
            provider = new Provider();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveNewProvider()
        {
            provider.Cod = db.Providers.OrderBy(c => c.Cod).Last().Cod + 1 ;
            provider.entity = entity;
            entity.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
            db.Entities.Add(entity);
            db.Providers.Add(provider);
            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new ProviderItem.ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem.ProviderItem_New.View.TS_PRO_Item_New();
                    MC_Page = new ProviderItem.ProviderItem_New.View.MC_PRO_Item_New() ;
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
        }

        override public void EV_ActivateSaveButton(bool verificated)
        {
            var a = (ProviderItem_New.View.TS_PRO_Item_New)LeftSide.Content;
            a.EnableButtonSaveUser(verificated);
        }
    }
}