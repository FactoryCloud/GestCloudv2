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

namespace GestCloudv2.Files.Nodes.Products.ProductMenu.View
{
    /// <summary>
    /// Interaction logic for MC_STR_Menu.xaml
    /// </summary>
    public partial class MC_PDT_Menu : Page
    {
        public MC_PDT_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_Products.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_Products.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().product != null)
            {
                DG_Products.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_ProductLoad();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_Products.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Products.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetProduct(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_Products.ItemsSource = null;
            DG_Products.ItemsSource = GetController().productsView.GetTable();
        }

        private Files.Nodes.Products.ProductMenu.Controller.CT_ProductMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Products.ProductMenu.Controller.CT_ProductMenu)a.MainFrame.Content;
        }
    }
}
