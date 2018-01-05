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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CLI_Item_New_Client.xaml
    /// </summary>
    public partial class MC_CLI_Item_New_Client : Page
    {
        public Client client;

        public MC_CLI_Item_New_Client()
        {
            InitializeComponent();
            client = new Client();
            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_ClientCod.KeyUp += new KeyEventHandler(EV_ClientCod);

            //CB_NormalTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_NormalTaxUpdate);
            //CB_SpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SpecialTaxUpdate);
            //CB_EquivalenceSurcharge.SelectionChanged += new SelectionChangedEventHandler(EV_CB_EquivalenceSurchargeUpdate);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ClientCod.Text = GetController().LastClientCod().ToString();
            //CB_NormalTax.SelectedIndex = GetController().normalTax;
            //CB_SpecialTax.SelectedIndex = GetController().specialTax;
            //CB_EquivalenceSurcharge.SelectedIndex = GetController().equivalenceSurcharge;
        }

        private void EV_ClientCod(object sender, RoutedEventArgs e)
        {
            if (TB_ClientCod.Text.Length == 0)
            {
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(TB_ClientCod.Text, "[^0-9]"))
                {
                    if (SP_Clientcod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Solo se permiten números";
                        SP_Clientcod.Children.Add(message);
                    }
                    TB_ClientCod.Text = TB_ClientCod.Text.Remove(TB_ClientCod.Text.Length - 1);
                }

                else if (GetController().ClientControlExist(int.Parse(TB_ClientCod.Text)))
                {
                    if (SP_Clientcod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_Clientcod.Children.Add(message);
                    }

                    else if (SP_Clientcod.Children.Count == 2)
                    {
                        SP_Clientcod.Children.RemoveAt(SP_Clientcod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_Clientcod.Children.Add(message);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }

                else
                {
                    if (SP_Clientcod.Children.Count == 2)
                    {
                        SP_Clientcod.Children.RemoveAt(SP_Clientcod.Children.Count - 1);
                    }
                }

                if (int.TryParse(TB_ClientCod.Text, out int d))
                {
                    client.Code = int.Parse(TB_ClientCod.Text);
                }
            }
        }

        /*private void EV_CB_NormalTaxUpdate(object sender, RoutedEventArgs e)
        {
            if (CB_NormalTax.SelectedIndex == 1)
            {
                MessageBox.Show("Vale 1");
                GetController().normalTax = 1; //NO
            }

            else
            {
                MessageBox.Show("Vale 0");
                GetController().normalTax = 0; //SI
            }
        }

        private void EV_CB_SpecialTaxUpdate(object sender, RoutedEventArgs e)
        {
            if (CB_SpecialTax.SelectedIndex == 1)
            {
                GetController().specialTax = 1; //NO
            }

            else
            {
                GetController().specialTax = 0; //SI
            }
        }

        private void EV_CB_EquivalenceSurchargeUpdate(object sender, RoutedEventArgs e)
        {
            if (CB_EquivalenceSurcharge.SelectedIndex == 1)
            {
                GetController().equivalenceSurcharge = 1; //NO
            }

            else
            {
                GetController().equivalenceSurcharge = 0; //SI
            }
        }*/

        private ClientItem_New.Controller.CT_CLI_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ClientItem_New.Controller.CT_CLI_Item_New)a.MainFrame.Content;
        }
    }
}
