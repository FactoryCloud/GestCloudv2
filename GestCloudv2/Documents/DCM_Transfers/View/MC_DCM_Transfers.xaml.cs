using System;
using System.Collections.Generic;
using System.Data;
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

namespace GestCloudv2.Documents.DCM_Transfers.View
{
    /// <summary>
    /// Interaction logic for MC_DCM_Menu.xaml
    /// </summary>
    public partial class MC_DCM_Transfers : Page
    {
        public MC_DCM_Transfers()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_Items.MouseLeftButtonUp += new MouseButtonEventHandler(EV_DocumentSelected);

            if (GetController().GetDocumentsCount() > 0)
            {
                BT_Delivery.IsEnabled = true;
                BT_Invoice.IsEnabled = true;
            }

            if (GetController().InvoiceExist())
                TB_DocumentCode.Text = GetController().GetInvoiceCode();

            if (GetController().DeliveryExist())
                TB_DocumentCode.Text = GetController().GetDeliveryCode();
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_SelectInvoice(object sender, RoutedEventArgs e)
        {
            GetController().EV_PurchaseInvoice();
            GetController().EV_SaleInvoice();
        }

        private void EV_SelectDelivery(object sender, RoutedEventArgs e)
        {
            GetController().EV_PurchaseDelivery();
            GetController().EV_SaleDelivery();
        }

        private void EV_DocumentSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_Items.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Items.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetItem(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_Items.ItemsSource = null;
            DG_Items.ItemsSource = GetController().itemsView.GetTable();
        }

        virtual public Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Transfers)a.MainFrame.Content;
        }
    }
}
