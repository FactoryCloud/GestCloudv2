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
using System.Windows.Shapes;
using FrameworkDB.V1;
using FrameworkView.V1;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for ProductSelectWindow.xaml
    /// </summary>
    public partial class ClientSelectWindow : Window
    {
        public ClientsView clientView;
        public Client client;
        public int clientSelected;

        public ClientSelectWindow()
        {
        } 

        public ClientSelectWindow(int option)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_ClientsView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_ClientsViewSelect);
            clientSelected = 0;
            clientView = new ClientsView();
        }

        public ClientSelectWindow(int option, int client)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_ClientsView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_ClientsViewSelect);
            clientSelected = client;
            clientView = new ClientsView();
        }
        protected void EV_Start(object sender, RoutedEventArgs e)
        {
          UpdateData(); 
        }

        public void EV_ClientsViewSelect(object sender, RoutedEventArgs e)
        {
            int client = DG_ClientsView.SelectedIndex;
            if (client >= 0)
            {
                DataGridRow row = (DataGridRow)DG_ClientsView.ItemContainerGenerator.ContainerFromIndex(client);
                DataRowView dr = row.Item as DataRowView;
                clientView.client = clientView.GetClient(Int32.Parse(dr.Row.ItemArray[0].ToString()));
                TB_ClientName.Text = clientView.client.entity.Name;
                BT_SelectClient.IsEnabled = true;
            }
        }

        private void EV_SelectClient(object sender, RoutedEventArgs e)
        {
            GetController().EV_SetClient(clientView.client.ClientID);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_ClientsView.ItemsSource = null;
            DG_ClientsView.ItemsSource = clientView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
