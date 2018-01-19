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
    public partial class SaleInvoiceSelectWindow : Window
    {
        public SaleInvoicesView saleInvoiceView;
        public SaleInvoice saleInvoice;
        public int SaleInvoiceSelected;

        public SaleInvoiceSelectWindow(Client client)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_SaleInvoiceView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SaleInvoicesViewSelect);
            SaleInvoiceSelected = 0;
            saleInvoiceView = new SaleInvoicesView(client);
        } 

        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData(); 
        }

        public void EV_SaleInvoicesViewSelect(object sender, RoutedEventArgs e)
        {
            int SaleInvoice = DG_SaleInvoiceView.SelectedIndex;
            if (SaleInvoice >= 0)
            {
                DataGridRow row = (DataGridRow)DG_SaleInvoiceView.ItemContainerGenerator.ContainerFromIndex(SaleInvoice);
                DataRowView dr = row.Item as DataRowView;
                SaleInvoiceSelected = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                BT_SelectSaleInvoice.IsEnabled = true;
            }
        }

        private void EV_SelectSaleInvoice(object sender, RoutedEventArgs e)
        {
            GetController().SetSaleInvoice(SaleInvoiceSelected);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_SaleInvoiceView.ItemsSource = null;
            DG_SaleInvoiceView.ItemsSource = saleInvoiceView.GetTable();
        }

        virtual public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            return new Documents.DCM_Transfers.Controller.CT_DCM_Transfers();
        }
    }
}
