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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_New_User.xaml
    /// </summary>
    public partial class MC_CLI_Item_Load_Client : Page
    {
        public MC_CLI_Item_Load_Client()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_ClientCod.KeyUp += new KeyEventHandler(EV_ClientCod);
            TB_ClientCod.Loaded += new RoutedEventHandler(EV_ClientCod);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ClientCod.Text = $"{GetController().client.Cod}";

            if (GetController().Information["editable"] == 0)
            {
                TB_ClientCod.IsReadOnly = true;
            }
        }

        private void EV_ClientCod(object sender, RoutedEventArgs e)
        {
            if (GetController().Information["editable"] == 1)
            {
                if (TB_ClientCod.Text.Length == 0)
                {
                    if (SP_ClientCod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede estar vacio";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCod.Children.Add(message);
                    }

                    else if (SP_ClientCod.Children.Count == 2)
                    {
                        SP_ClientCod.Children.RemoveAt(SP_ClientCod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede estar vacio";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCod.Children.Add(message);
                    }
                    GetController().CleanCod();
                }

                else if (TB_ClientCod.Text.Any(x => Char.IsWhiteSpace(x)))
                {
                    if (SP_ClientCod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede contener espacios";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCod.Children.Add(message);
                    }

                    else if (SP_ClientCod.Children.Count == 2)
                    {
                        SP_ClientCod.Children.RemoveAt(SP_ClientCod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede contener espacios";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCod.Children.Add(message);
                    }
                    GetController().CleanCod();
                }
                
                else if (GetController().UserControlExist(Convert.ToInt32(TB_ClientCod.Text)))
                {
                    if (SP_ClientCod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCod.Children.Add(message);
                    }

                    else if (SP_ClientCod.Children.Count == 2)
                    {
                        SP_ClientCod.Children.RemoveAt(SP_ClientCod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCod.Children.Add(message);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }

                else
                {
                    if (SP_ClientCod.Children.Count == 2)
                    {
                        SP_ClientCod.Children.RemoveAt(SP_ClientCod.Children.Count - 1);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }
            }
        }

        private Controller.CT_CLI_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CLI_Item_Load)a.MainFrame.Content;
        }
    }
}
