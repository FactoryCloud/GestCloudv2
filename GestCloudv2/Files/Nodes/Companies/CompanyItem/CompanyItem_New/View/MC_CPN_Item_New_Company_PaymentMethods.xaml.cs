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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_CPN_Item_New_Company_PaymentMethods : Page
    {
        public MC_CPN_Item_New_Company_PaymentMethods()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            foreach (PaymentMethod paymentMethod in GetController().GetPaymentMethods())
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
                label.Content = $"{paymentMethod.Code} - {paymentMethod.Name}";
                label.VerticalContentAlignment = VerticalAlignment.Center;
                Grid.SetColumn(label, 0);

                CheckBox checkbox = new CheckBox();
                checkbox.Tag = $"paymentMethod{paymentMethod.PaymentMethodID}";
                checkbox.Margin = new Thickness(10);
                checkbox.VerticalAlignment = VerticalAlignment.Center;

                if (GetController().paymentMethods.Contains(paymentMethod))
                    checkbox.IsChecked = true;

                checkbox.Checked += new RoutedEventHandler(EV_PaymentMethodChange);
                checkbox.Unchecked += new RoutedEventHandler(EV_PaymentMethodChange);

                Grid.SetColumn(checkbox, 2);

                grid.Children.Add(label);
                grid.Children.Add(checkbox);

                SP_CompanyName.Children.Add(grid);
            }
        }

        private void EV_PaymentMethodChange(object sender, RoutedEventArgs e)
        {
            GetController().UpdatePaymentMethod(Convert.ToInt32((sender as CheckBox).Tag.ToString().Replace("paymentMethod", "")));
        }

        private void EV_MD_PaymentMethodsAll(object sender, RoutedEventArgs e)
        {
            GetController().MD_PaymentMethodsChange(1);
        }

        private void EV_MD_PaymentMethodsNone(object sender, RoutedEventArgs e)
        {
            GetController().MD_PaymentMethodsChange(0);
        }

        private Controller.CT_CPN_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_New)a.MainFrame.Content;
        }
    }
}
