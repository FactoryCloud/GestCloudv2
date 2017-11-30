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
using GestCloudv2;
using FrameworkDB.V1;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderItem.PurchaseOrderItem_New.View
{
    /// <summary>
    /// Interaction logic for NV_POR_Item_New_PurchaseOrder_Submenu.xaml
    /// </summary>
    public partial class NV_POR_Item_New_PurchaseOrder_Submenu : Page
    {
        public NV_POR_Item_New_PurchaseOrder_Submenu()
        {
            InitializeComponent();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            ColumnDefinition column4 = new ColumnDefinition();
            ColumnDefinition column5 = new ColumnDefinition();
            ColumnDefinition column6 = new ColumnDefinition();
            column1.Width = new GridLength(1, GridUnitType.Star);
            column2.Width = new GridLength(1, GridUnitType.Star);
            column3.Width = new GridLength(1, GridUnitType.Star);
            column4.Width = new GridLength(1, GridUnitType.Star);
            column5.Width = new GridLength(1, GridUnitType.Star);
            column6.Width = new GridLength(1, GridUnitType.Star);

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);
            grid.ColumnDefinitions.Add(column3);
            grid.ColumnDefinitions.Add(column4);
            grid.ColumnDefinitions.Add(column5);
            grid.ColumnDefinitions.Add(column6);
            Grid.SetRow(grid, 1);

            Button button1 = new Button
            {
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20)
            };
            Grid.SetColumn(button1, 0);

            Button button2 = new Button
            {
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20)
            };
            Grid.SetColumn(button2, 1);

            Button button6 = new Button
            {
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20),
                IsEnabled = false
            };
            Grid.SetColumn(button6, 5);

            if (GetController().Information["submode"] == 3)
            {
                button1.Content = "Datos Básicos";
                button2.Content = "Stock Disponible";
                button6.Content = "Almacén";

                grid.Children.Add(button1);
                grid.Children.Add(button2);
                grid.Children.Add(button6);

                GR_Submenu.Children.Add(grid);
            }

            else if (GetController().Information["submode"] == 4)
            {
                button1.Content = "Datos Básicos";
                button2.Content = "Compras Recientes";
                button6.Content = "Proveedor";

                grid.Children.Add(button1);
                grid.Children.Add(button2);
                grid.Children.Add(button6);

                GR_Submenu.Children.Add(grid);
            }
        }

        private void EV_MD_Headboard(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1);
        }

        private void EV_MD_Movements(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Controller.CT_POR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Item_New)a.MainFrame.Content;
        }
    }
}
