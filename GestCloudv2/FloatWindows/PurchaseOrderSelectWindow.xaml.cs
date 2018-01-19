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
    public partial class PurchaseOrderSelectWindow : Window
    {
        public PurchaseOrdersView purchaseOrderView;
        public int purchaseOrder;
        public int PurchaseOrderSelected;

        public PurchaseOrderSelectWindow()
        {
        } 

        public PurchaseOrderSelectWindow(List<PurchaseOrder> Documents, Provider provider)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_PurchaseOrderView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_PurchaseOrdersViewSelect);
            PurchaseOrderSelected = 0;
            purchaseOrderView = new PurchaseOrdersView(Documents, provider);
        }

        public PurchaseOrderSelectWindow(int option, int PurchaseOrder)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_PurchaseOrderView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_PurchaseOrdersViewSelect);
            PurchaseOrderSelected = PurchaseOrder;
            purchaseOrderView = new PurchaseOrdersView();
        }
        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData(); 
        }

        public void EV_PurchaseOrdersViewSelect(object sender, RoutedEventArgs e)
        {
            int PurchaseOrder = DG_PurchaseOrderView.SelectedIndex;
            if (PurchaseOrder >= 0)
            {
                DataGridRow row = (DataGridRow)DG_PurchaseOrderView.ItemContainerGenerator.ContainerFromIndex(PurchaseOrder);
                DataRowView dr = row.Item as DataRowView;
                purchaseOrder = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                BT_SelectPurchaseOrder.IsEnabled = true;
            }
        }

        private void EV_SelectPurchaseOrder(object sender, RoutedEventArgs e)
        {
            GetController().EV_PurchaseOrderAdd(purchaseOrder);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_PurchaseOrderView.ItemsSource = null;
            DG_PurchaseOrderView.ItemsSource = purchaseOrderView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
