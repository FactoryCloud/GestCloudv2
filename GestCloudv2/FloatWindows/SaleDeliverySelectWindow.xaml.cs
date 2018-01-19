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
    public partial class SaleDeliverySelectWindow : Window
    {
        public SaleDeliveriesView saleDeliveryView;
        public int saleDelivery;

        public SaleDeliverySelectWindow()
        {
        }

        public SaleDeliverySelectWindow(Client client)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_SaleDeliveryView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SaleDeliverysViewSelect);
            saleDelivery = 0;
            saleDeliveryView = new SaleDeliveriesView(client);
        }

        public SaleDeliverySelectWindow(List<SaleDelivery> Documents, Client client)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_SaleDeliveryView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SaleDeliverysViewSelect);
            saleDeliveryView = new SaleDeliveriesView(Documents, client);
        }

        public SaleDeliverySelectWindow(int option, int SaleDelivery)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_SaleDeliveryView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SaleDeliverysViewSelect);
            this.saleDelivery = SaleDelivery;
            saleDeliveryView = new SaleDeliveriesView();
        }
        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData(); 
        }

        public void EV_SaleDeliverysViewSelect(object sender, RoutedEventArgs e)
        {
            int SaleDelivery = DG_SaleDeliveryView.SelectedIndex;
            if (SaleDelivery >= 0)
            {
                DataGridRow row = (DataGridRow)DG_SaleDeliveryView.ItemContainerGenerator.ContainerFromIndex(SaleDelivery);
                DataRowView dr = row.Item as DataRowView;
                saleDelivery = Int32.Parse(dr.Row.ItemArray[0].ToString());
                //TB_ClientName.Text = SaleDeliveryView.SaleDelivery.entity.Name;
                BT_SelectSaleDelivery.IsEnabled = true;
            }
        }

        private void EV_SelectSaleDelivery(object sender, RoutedEventArgs e)
        {
            GetController().EV_SaleDeliveryAdd(saleDelivery);
            GetController().SetSaleDelivery(saleDelivery);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_SaleDeliveryView.ItemsSource = null;
            DG_SaleDeliveryView.ItemsSource = saleDeliveryView.GetTable();
        }

        virtual public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            return new Documents.DCM_Transfers.Controller.CT_DCM_Transfers();
        }
    }
}
