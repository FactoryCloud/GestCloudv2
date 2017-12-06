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

namespace GestCloudv2.Files.Nodes.Stores.StoreItem.StoreItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_STR_Item_Load_Store_Companies : Page
    {
        int external;
        public MC_STR_Item_Load_Store_Companies(int external)
        {
            InitializeComponent();
            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);  
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            if (GetController().Information["editable"] == 0)
            {
                BT_SelectAll.Visibility = Visibility.Hidden;
                BT_SelectNone.Visibility = Visibility.Hidden;
            }

            foreach (Company company in GetController().GetCompanies())
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
                label.Content = $"{company.Code} - {company.Name}";
                label.VerticalContentAlignment = VerticalAlignment.Center;
                Grid.SetColumn(label, 0);

                CheckBox checkbox = new CheckBox();
                checkbox.Tag = $"company{company.CompanyID}";
                checkbox.Margin = new Thickness(10);
                checkbox.VerticalAlignment = VerticalAlignment.Center;
                if (GetController().Information["editable"] == 0)
                    checkbox.IsEnabled = false;

                if (GetController().companies.Contains(company))
                    checkbox.IsChecked = true;

                checkbox.Checked += new RoutedEventHandler(EV_CompaniesChange);
                checkbox.Unchecked += new RoutedEventHandler(EV_CompaniesChange);

                Grid.SetColumn(checkbox, 2);

                grid.Children.Add(label);
                grid.Children.Add(checkbox);

                SP_CompanyName.Children.Add(grid);
            }
        }

        private void EV_CompaniesChange(object sender, RoutedEventArgs e)
        {
            GetController().UpdateCompanies(Convert.ToInt32((sender as CheckBox).Tag.ToString().Replace("company", "")));
        }

        private void EV_MD_StoresAll(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoresChange(1);
        }

        private void EV_MD_StoresNone(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoresChange(0);
        }

        private Controller.CT_STR_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_STR_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_STR_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
