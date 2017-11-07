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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_USR_Item_New.xaml
    /// </summary>
    public partial class CT_CLI_Item_New : Main.Controller.CT_Common
    {
        public Client client;

        public CT_CLI_Item_New()
        {
            entity = new Entity();
            client = new Client();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveNewClient()
        {
            if (db.Clients.ToList().Count > 0)
            {
                client.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
            }
            else
            {
                client.Cod = 1;
            }

            if (db.Entities.ToList().Count > 0)
            {
                entity.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
            }
            else
            {
                entity.Cod = 1;
            }

            client.entity = entity;
            db.Entities.Add(entity);
            db.Clients.Add(client);
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
                    NV_Page = new ClientItem.ClientItem_New.View.NV_CLI_Item_New();
                    TS_Page = new ClientItem.ClientItem_New.View.TS_CLI_Item_New();
                    MC_Page = new ClientItem.ClientItem_New.View.MC_CLI_Item_New() ;
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
                    a.MainFrame.Content = new ClientMenu.Controller.CT_ClientMenu();
                    break;
            }
        }

        override public void EV_ActivateSaveButton(bool verificated)
        {
            var a = (ClientItem_New.View.TS_CLI_Item_New)LeftSide.Content;
            a.EnableButtonSaveUser(verificated);
        }
    }
}