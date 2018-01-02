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

namespace GestCloudv2.Files.Nodes.Products.ProductItem.ProductItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_PDT_Item_Load_Product : Page
    {
        int external;
        public MC_PDT_Item_Load_Product(int external)
        {
            InitializeComponent();

            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);

            TB_ProductCode.KeyUp += new KeyEventHandler(EV_ProductCode);
            TB_ProductCode.Loaded += new RoutedEventHandler(EV_ProductCode);

            TB_ProductName.KeyUp += new KeyEventHandler(EV_ProductCode);
            TB_ProductName.Loaded += new RoutedEventHandler(EV_ProductCode);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ProductName.Text = GetController().product.Name;
            TB_ProductCode.Text = $"{GetController().product.Code}";

            if (GetController().Information["editable"] == 0)
            {
                TB_ProductName.IsReadOnly = true;
                TB_ProductCode.IsReadOnly = true;
            }
        }

        private void EV_ProductCode(object sender, RoutedEventArgs e)
        {
            if(TB_ProductCode.Text.Length == 0)
            {
                if (SP_ProductCode.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductCode.Children.Add(message);
                }

                else if (SP_ProductCode.Children.Count == 2)
                {
                    SP_ProductCode.Children.RemoveAt(SP_ProductCode.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductCode.Children.Add(message);
                }
                GetController().CleanCode();
            }

            else if (GetController().ProductControlExist(TB_ProductName.Text))
            {
                if (SP_ProductName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este producto ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductName.Children.Add(message);
                }

                else if (SP_ProductName.Children.Count == 2)
                {
                    SP_ProductName.Children.RemoveAt(SP_ProductName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este producto ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductName.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_ProductName.Children.Count == 2)
                {
                    SP_ProductName.Children.RemoveAt(SP_ProductName.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private Controller.CT_PDT_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PDT_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PDT_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
