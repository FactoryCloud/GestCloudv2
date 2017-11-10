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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameworkDB.V1;

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company_Stores.xaml
    /// </summary>
    public partial class MC_CPN_Item_Load_Company_Stores : Page
    {
        public MC_CPN_Item_Load_Company_Stores()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            foreach(Store store in GetController().GetStores())
            {
                Grid grid = new Grid();
                ColumnDefinition column1 = new ColumnDefinition();
                ColumnDefinition column2 = new ColumnDefinition();
                ColumnDefinition column3 = new ColumnDefinition();
                column1.Width = new GridLength(2, GridUnitType.Star);
                column2.Width = new GridLength(1, GridUnitType.Star);
                column3.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                grid.ColumnDefinitions.Add(column3);
                grid.Margin = new Thickness(15);
                grid.MinHeight = 100;

                Label label = new Label();
                label.Content = $"{store.Code} - {store.Name}";
                label.VerticalContentAlignment = VerticalAlignment.Center;
                Grid.SetColumn(label, 0);

                CheckBox checkbox = new CheckBox();
                checkbox.Tag = $"store{store.StoreID}";
                checkbox.Margin = new Thickness(10);
                checkbox.VerticalAlignment = VerticalAlignment.Center;

                if (GetController().stores.Contains(store))
                    checkbox.IsChecked = true;

                checkbox.Checked += new RoutedEventHandler(EV_StoresChange);
                checkbox.Unchecked += new RoutedEventHandler(EV_StoresChange);

                Grid.SetColumn(checkbox, 2);

                grid.Children.Add(label);
                grid.Children.Add(checkbox);

                SP_CompanyName.Children.Add(grid);
            }
        }

        private void EV_StoresChange(object sender, RoutedEventArgs e)
        {
            GetController().UpdateStore(Convert.ToInt32((sender as CheckBox).Tag.ToString().Replace("store", "")));
        }

        private void EV_MD_StoresAll(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoresChange(1);
        }

        private void EV_MD_StoresNone(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoresChange(0);
        }

        private Controller.CT_CPN_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_Load)a.MainFrame.Content;
        }
    }
}
