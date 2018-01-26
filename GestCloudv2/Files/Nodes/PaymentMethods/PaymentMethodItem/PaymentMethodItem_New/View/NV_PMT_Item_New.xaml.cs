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

namespace GestCloudv2.Files.Nodes.PaymentMethods.PaymentMethodItem.PaymentMethodItem_New.View
{
    /// <summary>
    /// Interaction logic for NV_STR_Item_New.xaml
    /// </summary>
    public partial class NV_PMT_Item_New : Page
    {
        public NV_PMT_Item_New()
        {
            InitializeComponent();
            foreach (SubmenuItem item in GetController().submenuItems.GetSubmenuItems(5))
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
                GR_Navigation.Children.Add(temp);
            }

            Button subtitle = new Button
            {
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20)
            };
            Grid.SetColumn(subtitle, 5);
            subtitle.Content = "Volver";
            subtitle.Click += new RoutedEventHandler(EV_CT_Menu);
            GR_Navigation.Children.Add(subtitle);
        }

        private void EV_MD_Submenu(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(Convert.ToInt16(((Button)sender).Tag),1);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Controller.CT_PMT_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PMT_Item_New)a.MainFrame.Content;
        }
    }
}
