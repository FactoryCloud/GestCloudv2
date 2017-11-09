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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_New_User.xaml
    /// </summary>
    public partial class MC_PRO_Item_Load_Provider : Page
    {
        public MC_PRO_Item_Load_Provider()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_ProviderCod.KeyUp += new KeyEventHandler(EV_ClientCod);
            TB_ProviderCod.Loaded += new RoutedEventHandler(EV_ClientCod);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ProviderCod.Text = $"{GetController().provider.Cod}";

            if (GetController().Information["editable"] == 0)
            {
                TB_ProviderCod.IsReadOnly = true;
            }
        }

        private void EV_ClientCod(object sender, RoutedEventArgs e)
        {
            if (GetController().Information["editable"] == 1)
            {
                if (TB_ProviderCod.Text.Length == 0)
                {
                    if (SP_ProviderCod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede estar vacio";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ProviderCod.Children.Add(message);
                    }

                    else if (SP_ProviderCod.Children.Count == 2)
                    {
                        SP_ProviderCod.Children.RemoveAt(SP_ProviderCod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede estar vacio";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ProviderCod.Children.Add(message);
                    }
                    GetController().CleanCod();
                }

                else if (TB_ProviderCod.Text.Any(x => Char.IsWhiteSpace(x)))
                {
                    if (SP_ProviderCod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede contener espacios";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ProviderCod.Children.Add(message);
                    }

                    else if (SP_ProviderCod.Children.Count == 2)
                    {
                        SP_ProviderCod.Children.RemoveAt(SP_ProviderCod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede contener espacios";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ProviderCod.Children.Add(message);
                    }
                    GetController().CleanCod();
                }
                
                else if (GetController().ProviderControlExist(Convert.ToInt32(TB_ProviderCod.Text)))
                {
                    if (SP_ProviderCod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ProviderCod.Children.Add(message);
                    }

                    else if (SP_ProviderCod.Children.Count == 2)
                    {
                        SP_ProviderCod.Children.RemoveAt(SP_ProviderCod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ProviderCod.Children.Add(message);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }

                else
                {
                    if (SP_ProviderCod.Children.Count == 2)
                    {
                        SP_ProviderCod.Children.RemoveAt(SP_ProviderCod.Children.Count - 1);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }
            }
        }

        private Controller.CT_PRO_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PRO_Item_Load)a.MainFrame.Content;
        }
    }
}
