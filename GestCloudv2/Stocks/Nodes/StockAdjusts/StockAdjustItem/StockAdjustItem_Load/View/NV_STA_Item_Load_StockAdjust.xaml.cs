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
using FrameworkView.V1;

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_Load.View
{
    /// <summary>
    /// Interaction logic for NV_STA_Item_New_StockAdjust.xaml
    /// </summary>
    public partial class NV_STA_Item_Load_StockAdjust : Page
    {
        public NV_STA_Item_Load_StockAdjust()
        {
            InitializeComponent();

            if (GetController().Information["submenu"] == 1)
            {
                RowDefinition row1 = new RowDefinition();
                RowDefinition row2 = new RowDefinition();
                row1.Height = new GridLength(1, GridUnitType.Star);
                row2.Height = new GridLength(2, GridUnitType.Star);
                GR_Navigation.RowDefinitions.Add(row1);
                GR_Navigation.RowDefinitions.Add(row2);

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

                Grid GR_Submenu = new Grid();
                GR_Submenu.ColumnDefinitions.Add(column1);
                GR_Submenu.ColumnDefinitions.Add(column2);
                GR_Submenu.ColumnDefinitions.Add(column3);
                GR_Submenu.ColumnDefinitions.Add(column4);
                GR_Submenu.ColumnDefinitions.Add(column5);
                GR_Submenu.ColumnDefinitions.Add(column6);

                Grid.SetColumnSpan(GR_Submenu, 6);
                Grid.SetRow(GR_Submenu, 1);


                foreach (SubmenuItem item in GetController().CT_Submenu.items)
                {
                    Button temp = new Button
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(20)
                    };
                    Grid.SetColumn(temp, item.Option - 1);

                    temp.Content = item.Content;
                    temp.Name = item.Name;
                    temp.Tag = item.Option;
                    temp.Click += new RoutedEventHandler(EV_MD_Submenu);
                    GR_Submenu.Children.Add(temp);
                }

                Button subtitle = new Button
                {
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20)
                };
                Grid.SetColumn(subtitle, 5);
                subtitle.Content = GetController().CT_Submenu.Name;
                subtitle.IsEnabled = false;
                GR_Submenu.Children.Add(subtitle);

                GR_Navigation.Children.Add(GR_Submenu);
            }
        }

        private void EV_MD_Submenu(object sender, RoutedEventArgs e)
        {
            GetController().MD_Submenu(Convert.ToInt16(((Button)sender).Tag));
        }

        private void EV_MD_StockAdjust(object sender, RoutedEventArgs e)
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

        private Controller.CT_STA_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STA_Item_Load)a.MainFrame.Content;
        }
    }
}
