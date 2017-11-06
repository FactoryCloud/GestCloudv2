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

namespace GestCloudv2.Files.Nodes.Providers.ProviderMenu.View
{
    /// <summary>
    /// Interaction logic for MC_Client.xaml
    /// </summary>
    public partial class MC_Provider : Page
    {
        ProvidersView providerView;
        Provider providers;
        GestCloudDB db;

        public MC_Provider()
        {
            providerView = new ProvidersView();
            providers = new Provider();
            InitializeComponent();
            DG_Providers.MouseLeftButtonUp += new MouseButtonEventHandler(ProviderSelected_Event);
            //DG_Clients.MouseDoubleClick += new MouseButtonEventHandler(ProviderInfo_Event);
            this.Loaded += new RoutedEventHandler(UpdateTable);
        }

        public void UpdateData()
        {
            DG_Providers.ItemsSource = null;
            DG_Providers.ItemsSource = providerView.GetTable();
        }

        private void UpdateTable(Object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void ProviderSelected_Event(object sender, RoutedEventArgs e)
        {
            int provider = DG_Providers.SelectedIndex;
            if (provider >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Providers.ItemContainerGenerator.ContainerFromIndex(provider);
                DataRowView dr = row.Item as DataRowView;
                //MessageBox.Show(dr.Row.ItemArray[0].ToString());
                providers.ProvidersID = Int32.Parse(dr.Row.ItemArray[0].ToString());
                providers.EntityID = Int32.Parse(dr.Row.ItemArray[0].ToString());
            }
        }

        private void ProviderInfo_Event(object sender, MouseButtonEventArgs e)
        {
            SelectedProviderUpdate();
            ProviderInfoLoad();
        }

        public void SelectedProviderUpdate()
        {
            int provider = DG_Providers.SelectedIndex;
            if (provider >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Providers.ItemContainerGenerator.ContainerFromIndex(provider);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetProvider(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        public void ProviderInfoLoad()
        {
            int provider = DG_Providers.SelectedIndex;
            if (provider >= 0)
            {
                //GetController().StartViewUser();
            }
        }

        private ProviderMenu.Controller.CT_ProviderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ProviderMenu.Controller.CT_ProviderMenu)a.MainFrame.Content;
        }
    }
}
