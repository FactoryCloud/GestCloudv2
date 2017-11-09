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
using FrameworkView.V1;

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_CLI_Item_Load.xaml
    /// </summary>
    public partial class CT_CLI_Item_Load : Main.Controller.CT_Common
    {
        public Client client;

        public CT_CLI_Item_Load(Client client, int editable)
        {
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            this.client = client;
            Information["entityValid"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{entity.EntityID}");
            entity = db.Entities.Where(u => u.EntityID == client.EntityID).First();

            UpdateComponents();
        }

        public List<Client> GetClients()
        {
            return db.Clients.OrderBy(u => u.Cod).ToList();
        }

        public void SetClientCod(int num)
        {
            client.Cod = num;
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

        public void CleanCod()
        {
            client.Cod = 0;
            TestMinimalInformation();
        }

        public Boolean UserControlExist(int cod)
        {
            List<Client> clients = db.Clients.ToList();
            foreach (var item in clients)
            {
                if ((item.Cod == cod && client.Cod != cod)|| cod == 0)
                {
                    CleanCod();
                    return true;
                }
            }
            client.Cod = cod;
            TestMinimalInformation();
            return false;
        }

        private void TestMinimalInformation()
        {
            if (client.Cod > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }
            if (Information["editable"] != 0)
            {
                TS_Page = new View.TS_CLI_Item_Load_Editable(Information["minimalInformation"]);
                LeftSide.Content = TS_Page;
            }
        }

        public void SaveNewClient()
        {
            if (Information["entityLoaded"] == 2)
            {
                db.Entities.Update(entity);
            }
            
            Client client1 = db.Clients.Where(u => u.ClientID == client.ClientID).First();
            client1.Cod= client.Cod;
            client1.EntityID = entity.EntityID;

            db.Clients.Update(client1);
            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        override public void MD_EntityEdit()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3);
        }

        override public void MD_EntityLoad()
        {
            View.FW_CLI_Item_Load_Entity floatWindow = new View.FW_CLI_Item_Load_Entity(4);
            floatWindow.Show();
        }

        public override void MD_EntityLoaded()
        {
            MD_Change(4);
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            if (Information["entityLoaded"] == 1 && Information["mode"] == 2)
                Information["mode"] = 4;

            if(Information["entityLoaded"] == 2 && Information["mode"] == 2)
                Information["mode"] = 3;

            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_CLI_Item_Load();
                    if(Information["editable"] == 0)
                        TS_Page = new View.TS_CLI_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_CLI_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_CLI_Item_Load_Client();
                    ChangeComponents();
                    break;

                case 2:
                    if (Information["editable"] == 0)
                    {
                        Information["mode"] = 4;
                        UpdateComponents();
                        break;
                    }

                    else
                    {
                        NV_Page = new View.NV_CLI_Item_Load();
                        TS_Page = new View.TS_CLI_Item_Load_Editable(Information["minimalInformation"]);
                        MC_Page = new View.MC_CLI_Item_Load_Entity_Select();
                    }
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new View.NV_CLI_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_CLI_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_CLI_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_CLI_Item_Load_Entity_Edit();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new View.NV_CLI_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_CLI_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_CLI_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_CLI_Item_Load_Entity_Loaded();
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
                    a.MainFrame.Content = new Files.Nodes.Clients.ClientMenu.Controller.CT_ClientMenu();
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