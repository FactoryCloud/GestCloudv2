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
using FrameworkView.V1;
using System.Data;
using FrameworkDB.V1;

namespace GestCloudv2.Files.Nodes.Clients.ClientMenu.View
{
    /// <summary>
    /// Interaction logic for MC_Client.xaml
    /// </summary>
    public partial class MC_Client : Page
    {
        ClientsView clientsView;
        Client clients;
        GestCloudDB db;

        public MC_Client()
        {
            clientsView = new ClientsView();
            clients = new Client();
            InitializeComponent();
            DG_Clients.MouseLeftButtonUp += new MouseButtonEventHandler(ClientSelected_Event);
            this.Loaded += new RoutedEventHandler(UpdateTable);
        }

        public void UpdateData()
        {
            DG_Clients.ItemsSource = null;
            DG_Clients.ItemsSource = clientsView.GetTable();
        }

        private void UpdateTable(Object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void ClientSelected_Event(object sender, RoutedEventArgs e)
        {
            int num = DG_Clients.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Clients.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                //MessageBox.Show($"{Int32.Parse(dr.Row.ItemArray[0].ToString())}");
                GetController().SetClient(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private ClientMenu.Controller.CT_ClientMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ClientMenu.Controller.CT_ClientMenu)a.MainFrame.Content;
        }

    }
}
