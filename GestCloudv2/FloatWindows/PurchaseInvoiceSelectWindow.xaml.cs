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
    public partial class PurchaseInvoiceSelectWindow : Window
    {
        public PurchaseInvoicesView purchaseInvoiceView;
        public PurchaseInvoice purchaseInvoice;
        public int PurchaseInvoiceSelected;

        public PurchaseInvoiceSelectWindow(Provider provider)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_PurchaseInvoiceView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_PurchaseInvoicesViewSelect);
            PurchaseInvoiceSelected = 0;
            purchaseInvoiceView = new PurchaseInvoicesView(provider);
        } 

        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData(); 
        }

        public void EV_PurchaseInvoicesViewSelect(object sender, RoutedEventArgs e)
        {
            int PurchaseInvoice = DG_PurchaseInvoiceView.SelectedIndex;
            if (PurchaseInvoice >= 0)
            {
                DataGridRow row = (DataGridRow)DG_PurchaseInvoiceView.ItemContainerGenerator.ContainerFromIndex(PurchaseInvoice);
                DataRowView dr = row.Item as DataRowView;
                PurchaseInvoiceSelected = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                BT_SelectPurchaseInvoice.IsEnabled = true;
            }
        }

        private void EV_SelectPurchaseInvoice(object sender, RoutedEventArgs e)
        {
            GetController().SetPurchaseInvoice(PurchaseInvoiceSelected);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_PurchaseInvoiceView.ItemsSource = null;
            DG_PurchaseInvoiceView.ItemsSource = purchaseInvoiceView.GetTable();
        }

        virtual public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            return new Documents.DCM_Transfers.Controller.CT_DCM_Transfers();
        }
    }
}
