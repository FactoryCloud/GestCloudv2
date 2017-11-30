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
    /// Interaction logic for ProviderSelectWindow.xaml
    /// </summary>
    public partial class ProviderSelectWindow : Window
    {
        public ProvidersView providersView;
        public Provider provider;
        public int providerSelected;

        public ProviderSelectWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            //this.Closed += new EventHandler(EV_Close);
            DG_ProvidersView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_ProviderViewSelect);
            providerSelected = 0;
            providersView = new ProvidersView();
        }

        protected void EV_Start(object sender, RoutedEventArgs e)
        {
          UpdateData(); 
        }

        public void EV_ProviderViewSelect(object sender, RoutedEventArgs e)
        {
            int num = DG_ProvidersView.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_ProvidersView.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                providersView.provider = providersView.GetProvider(Int32.Parse(dr.Row.ItemArray[0].ToString()));
                TB_ProviderName.Text = providersView.provider.entity.Name;
                BT_SelectProvider.IsEnabled = true;

            }
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        protected void EV_SetProvider(object sender, RoutedEventArgs e)
        {
            GetController().EV_SetProvider(providersView.provider.ProviderID);

            this.Close();
        }

        public void UpdateData()
        {
            DG_ProvidersView.ItemsSource = null;
            DG_ProvidersView.ItemsSource = providersView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
