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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeMenu.View
{
    /// <summary>
    /// Interaction logic for MC_STR_Menu.xaml
    /// </summary>
    public partial class MC_PTY_Menu : Page
    {
        public MC_PTY_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_ProductTypes.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_ProductTypes.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().productyType != null)
            {
                DG_ProductTypes.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_ProductTypeLoad();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_ProductTypes.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_ProductTypes.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                //GetController().SetCompany(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_ProductTypes.ItemsSource = null;
            DG_ProductTypes.ItemsSource = GetController().productTypesView.GetTable();
        }

        private Files.Nodes.ProductTypes.ProductTypeMenu.Controller.CT_ProductTypeMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.ProductTypes.ProductTypeMenu.Controller.CT_ProductTypeMenu)a.MainFrame.Content;
        }
    }
}
