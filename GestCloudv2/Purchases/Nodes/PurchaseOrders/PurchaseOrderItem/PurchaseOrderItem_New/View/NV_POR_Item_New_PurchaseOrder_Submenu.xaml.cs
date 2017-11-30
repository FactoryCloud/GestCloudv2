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

            switch(GetController().Information["submenu"])
            {
                case 1:

                    Grid grid = new Grid();
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
                    grid.ColumnDefinitions.Add(column1);
                    grid.ColumnDefinitions.Add(column2);
                    grid.ColumnDefinitions.Add(column3);
                    grid.ColumnDefinitions.Add(column4);
                    grid.ColumnDefinitions.Add(column5);
                    grid.ColumnDefinitions.Add(column6);
                    Grid.SetRow(grid, 1);

                    Button button1 = new Button();
                    button1.VerticalContentAlignment = VerticalAlignment.Center;
                    button1.Content = "Datos Básicos";
                    button1.Margin = new Thickness(20);
                    Grid.SetColumn(button1, 0);

                    Button button2 = new Button();
                    button2.VerticalContentAlignment = VerticalAlignment.Center;
                    button2.Content = "Compras Recientes";
                    button2.Margin = new Thickness(20);
                    Grid.SetColumn(button2, 1);

                    Button button3 = new Button();
                    button3.VerticalContentAlignment = VerticalAlignment.Center;
                    button3.Content = "Proveedor";
                    button3.Margin = new Thickness(20);
                    Grid.SetColumn(button3, 5);

                    grid.Children.Add(button1);
                    grid.Children.Add(button2);
                    grid.Children.Add(button3);

                    GR_Submenu.Children.Add(grid);

                    break;
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
