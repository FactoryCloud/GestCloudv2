using FrameworkDB.V1;
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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CLI_Item_New_Client.xaml
    /// </summary>
    public partial class MC_PRO_Item_New_Provider : Page
    {
        public Provider provider;

        public MC_PRO_Item_New_Provider()
        {
            InitializeComponent();
            provider = new Provider();
            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_ProviderCod.KeyUp += new KeyEventHandler(EV_ClientCod);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ProviderCod.Text = GetController().LastProviderCod().ToString();
        }

        private void EV_ClientCod(object sender, RoutedEventArgs e)
        {
            if (TB_ProviderCod.Text.Length == 0)
            {
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(TB_ProviderCod.Text, "[^0-9]"))
                {
                    if (SP_Providercod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Solo se permiten números";
                        SP_Providercod.Children.Add(message);
                    }
                    TB_ProviderCod.Text = TB_ProviderCod.Text.Remove(TB_ProviderCod.Text.Length - 1);
                }

                else if (GetController().ProviderControlExist(int.Parse(TB_ProviderCod.Text)))
                {
                    if (SP_Providercod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_Providercod.Children.Add(message);
                    }

                    else if (SP_Providercod.Children.Count == 2)
                    {
                        SP_Providercod.Children.RemoveAt(SP_Providercod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_Providercod.Children.Add(message);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }

                else
                {
                    if (SP_Providercod.Children.Count == 2)
                    {
                        SP_Providercod.Children.RemoveAt(SP_Providercod.Children.Count - 1);
                    }
                }

                if (int.TryParse(TB_ProviderCod.Text, out int d))
                {
                    provider.Cod = int.Parse(TB_ProviderCod.Text);
                }
            }
        }

        private ProviderItem_New.Controller.CT_PRO_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ProviderItem_New.Controller.CT_PRO_Item_New)a.MainFrame.Content;
        }
    }
}
