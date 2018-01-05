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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_USR_Item_New.xaml
    /// </summary>
    public partial class CT_CLI_Item_New : Main.Controller.CT_Common
    {
        public Client client;
        public int lastClientCod;
        public SubmenuItems submenuItems;

        public CT_CLI_Item_New()
        {
            submenuItems = new SubmenuItems();
            entity = new Entity();
            client = new Client();
            Information.Add("minimalInformation", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveNewClient()
        {

            if (Information["entityLoaded"] == 2)
            {
                if (db.Clients.ToList().Count > 0)
                {
                    entity.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
                }
                else
                {
                    entity.Cod = 1;
                }

                db.Entities.Add(entity);
            }

            client.entity = entity;
            db.Clients.Add(client);
            /*if (normalTax >= 0)
            {
                db.ClientsTaxes.Add(
                    new ClientTax
                    {
                        client = client,
                        NormalTax = normalTax
                    });
            }

            if (specialTax >= 0)
            {
                db.ClientsTaxes.Add(
                    new ClientTax
                    {
                        client = client,
                        SpecialTax = specialTax
                    });
            }

            if (equivalenceSurcharge >= 0)
            {
                db.ClientsTaxes.Add(
                    new ClientTax
                    {
                        client = client,
                        EquivalenceSurcharge = equivalenceSurcharge
                    });
            }*/
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
            View.FW_CLI_Item_Load_Entity floatWindow = new View.FW_CLI_Item_Load_Entity(4);
            floatWindow.Show();
        }

        override public void MD_EntityNew()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3,0);
        }

        public override void MD_EntityLoaded()
        {
            MD_Change(4,0);
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

        public Boolean ClientControlExist(int clientCod)
        {
            List<Client> clients = db.Clients.ToList();
            foreach (var item in clients)
            {
                if (item.Code == clientCod)
                {
                    client.Code = 0;
                    return true;
                }
            }
            client.Code = clientCod;
            TestMinimalInformation();
            return false;
        }

        public int LastClientCod()
        {
            if (db.Clients.ToList().Count > 0)
            {
                lastClientCod = db.Clients.OrderBy(u => u.Code).Last().Code + 1;
                client.Code = lastClientCod;
                return lastClientCod;
            }
            else
            {
                client.Code = 1;
                return lastClientCod = 1;

            }
        }

        public void TestMinimalInformation()
        {
            if (client.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new View.TS_CLI_Item_New(Information["minimalInformation"]);
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
                    NV_Page = new ClientItem_New.View.NV_CLI_Item_New();
                    TS_Page = new ClientItem_New.View.TS_CLI_Item_New(Information["minimalInformation"]);
                    MC_Page = new ClientItem_New.View.MC_CLI_Item_New_Client();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new ClientItem_New.View.NV_CLI_Item_New();
                    TS_Page = new ClientItem_New.View.TS_CLI_Item_New(Information["minimalInformation"]);
                    MC_Page = new ClientItem_New.View.MC_CLI_Item_Load_Entity_Select();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new ClientItem_New.View.NV_CLI_Item_New();
                    TS_Page = new ClientItem_New.View.TS_CLI_Item_New(Information["minimalInformation"]);
                    MC_Page = new ClientItem_New.View.MC_CLI_Item_New_Entity_New();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new ClientItem_New.View.NV_CLI_Item_New();
                    TS_Page = new ClientItem_New.View.TS_CLI_Item_New(Information["minimalInformation"]);
                    MC_Page = new ClientItem_New.View.MC_CLI_Item_Load_Entity_Loaded();
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
    }
}