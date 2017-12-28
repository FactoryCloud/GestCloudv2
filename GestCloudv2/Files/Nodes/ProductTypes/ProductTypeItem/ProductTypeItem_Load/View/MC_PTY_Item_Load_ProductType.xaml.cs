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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_PTY_Item_Load_ProductType : Page
    {
        int external;
        public MC_PTY_Item_Load_ProductType(int external)
        {
            InitializeComponent();

            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);

            TB_ProductTypeCode.KeyUp += new KeyEventHandler(EV_ProductTypeCode);
            TB_ProductTypeCode.Loaded += new RoutedEventHandler(EV_ProductTypeCode);

            TB_ProductTypeName.KeyUp += new KeyEventHandler(EV_ProductTypeCode);
            TB_ProductTypeName.Loaded += new RoutedEventHandler(EV_ProductTypeCode);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ProductTypeName.Text = GetController().productType.Name;
            TB_ProductTypeCode.Text = $"{GetController().productType.Code}";

            if (GetController().Information["editable"] == 0)
            {
                TB_ProductTypeName.IsReadOnly = true;
                TB_ProductTypeCode.IsReadOnly = true;
            }
        }

        private void EV_ProductTypeCode(object sender, RoutedEventArgs e)
        {
            if(TB_ProductTypeCode.Text.Length == 0)
            {
                if (SP_ProductTypeCode.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductTypeCode.Children.Add(message);
                }

                else if (SP_ProductTypeCode.Children.Count == 2)
                {
                    SP_ProductTypeCode.Children.RemoveAt(SP_ProductTypeCode.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductTypeCode.Children.Add(message);
                }
                GetController().CleanCode();
            }

            else if (GetController().ProductTypeControlExist(TB_ProductTypeName.Text))
            {
                if (SP_ProductTypeName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este almacén ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductTypeName.Children.Add(message);
                }

                else if (SP_ProductTypeName.Children.Count == 2)
                {
                    SP_ProductTypeName.Children.RemoveAt(SP_ProductTypeName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este tipo de producto ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_ProductTypeName.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_ProductTypeName.Children.Count == 2)
                {
                    SP_ProductTypeName.Children.RemoveAt(SP_ProductTypeName.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private Controller.CT_PTY_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PTY_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PTY_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
