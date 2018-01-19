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
    public partial class PurchaseDeliverySelectWindow : Window
    {
        public PurchaseDeliveriesView purchaseDeliveryView;
        public int purchaseDelivery;

        public PurchaseDeliverySelectWindow()
        {
        }

        public PurchaseDeliverySelectWindow(Provider provider)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_PurchaseDeliveryView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_PurchaseDeliverysViewSelect);
            purchaseDelivery = 0;
            purchaseDeliveryView = new PurchaseDeliveriesView(provider);
        }

        public PurchaseDeliverySelectWindow(List<PurchaseDelivery> Documents, Provider provider)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_PurchaseDeliveryView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_PurchaseDeliverysViewSelect);
            purchaseDeliveryView = new PurchaseDeliveriesView(Documents, provider);
        }

        public PurchaseDeliverySelectWindow(int option, int PurchaseDelivery)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_PurchaseDeliveryView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_PurchaseDeliverysViewSelect);
            this.purchaseDelivery = PurchaseDelivery;
            purchaseDeliveryView = new PurchaseDeliveriesView();
        }
        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData(); 
        }

        public void EV_PurchaseDeliverysViewSelect(object sender, RoutedEventArgs e)
        {
            int PurchaseDelivery = DG_PurchaseDeliveryView.SelectedIndex;
            if (PurchaseDelivery >= 0)
            {
                DataGridRow row = (DataGridRow)DG_PurchaseDeliveryView.ItemContainerGenerator.ContainerFromIndex(PurchaseDelivery);
                DataRowView dr = row.Item as DataRowView;
                purchaseDelivery = Int32.Parse(dr.Row.ItemArray[0].ToString());
                //TB_ProviderName.Text = PurchaseDeliveryView.PurchaseDelivery.entity.Name;
                BT_SelectPurchaseDelivery.IsEnabled = true;
            }
        }

        private void EV_SelectPurchaseDelivery(object sender, RoutedEventArgs e)
        {
            GetController().EV_PurchaseDeliveryAdd(purchaseDelivery);
            GetController().SetPurchaseDelivery(purchaseDelivery);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_PurchaseDeliveryView.ItemsSource = null;
            DG_PurchaseDeliveryView.ItemsSource = purchaseDeliveryView.GetTable();
        }

        virtual public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            return new Documents.DCM_Transfers.Controller.CT_DCM_Transfers();
        }
    }
}
