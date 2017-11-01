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

namespace GestCloudv2.Files.Nodes.Stores.StoreMenu.View
{
    /// <summary>
    /// Interaction logic for MC_STR_Menu.xaml
    /// </summary>
    public partial class MC_STR_Menu : Page
    {
        public MC_STR_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_Stores.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_Stores.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().Store != null)
            {
                // Función al seleccionar empresa
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_Stores.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Stores.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetCompany(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_Stores.ItemsSource = null;
            DG_Stores.ItemsSource = GetController().StoresView.GetTable();
        }

        private Files.Nodes.Stores.StoreMenu.Controller.CT_StoreMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Stores.StoreMenu.Controller.CT_StoreMenu)a.MainFrame.Content;
        }
    }
}
