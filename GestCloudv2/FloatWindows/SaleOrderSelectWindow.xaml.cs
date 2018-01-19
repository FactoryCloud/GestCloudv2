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
    public partial class SaleOrderSelectWindow : Window
    {
        public SaleOrdersView saleOrderView;
        public int saleOrder;
        public int SaleOrderSelected;

        public SaleOrderSelectWindow()
        {
        } 

        public SaleOrderSelectWindow(List<SaleOrder> Documents, Client client)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_SaleOrderView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SaleOrdersViewSelect);
            SaleOrderSelected = 0;
            saleOrderView = new SaleOrdersView(Documents, client);
        }

        public SaleOrderSelectWindow(int option, int SaleOrder)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_SaleOrderView.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SaleOrdersViewSelect);
            SaleOrderSelected = SaleOrder;
            saleOrderView = new SaleOrdersView();
        }
        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData(); 
        }

        public void EV_SaleOrdersViewSelect(object sender, RoutedEventArgs e)
        {
            int SaleOrder = DG_SaleOrderView.SelectedIndex;
            if (SaleOrder >= 0)
            {
                DataGridRow row = (DataGridRow)DG_SaleOrderView.ItemContainerGenerator.ContainerFromIndex(SaleOrder);
                DataRowView dr = row.Item as DataRowView;
                saleOrder = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                BT_SelectSaleOrder.IsEnabled = true;
            }
        }

        private void EV_SelectSaleOrder(object sender, RoutedEventArgs e)
        {
            GetController().EV_SaleOrderAdd(saleOrder);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            
        }

        public void UpdateData()
        {
            DG_SaleOrderView.ItemsSource = null;
            DG_SaleOrderView.ItemsSource = saleOrderView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
