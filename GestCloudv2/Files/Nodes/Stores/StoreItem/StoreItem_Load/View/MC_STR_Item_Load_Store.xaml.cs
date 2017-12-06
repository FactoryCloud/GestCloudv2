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

namespace GestCloudv2.Files.Nodes.Stores.StoreItem.StoreItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_STR_Item_Load_Store : Page
    {
        int external;
        public MC_STR_Item_Load_Store(int external)
        {
            InitializeComponent();

            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_StoreCode.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            TB_StoreName.KeyUp += new KeyEventHandler(EV_StoreName);
            TB_StoreName.Loaded += new RoutedEventHandler(EV_StoreName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_StoreName.Text = GetController().store.Name;

            if (GetController().Information["editable"] == 0)
            {
                TB_StoreName.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_StoreCode = new TextBox();
                TB_StoreCode.Name = "TB_StoreCode";
                TB_StoreCode.Text = $"{GetController().store.Code}";
                TB_StoreCode.VerticalAlignment = VerticalAlignment.Center;
                TB_StoreCode.TextAlignment = TextAlignment.Center;
                TB_StoreCode.Margin = margin;
                Grid.SetColumn(TB_StoreCode, 2);
                Grid.SetRow(TB_StoreCode, 2);

                GR_Main.Children.Add(TB_StoreCode);

                CB_StoreCode.Visibility = Visibility.Hidden;
            }

            else
            {
                List<Store> stores = GetController().GetStores();
                List<int> nums = new List<int>();
                foreach (var sto in stores)
                {
                    if(sto.StoreID != GetController().store.StoreID)
                        nums.Add(Convert.ToInt16(sto.Code));
                }

                for (int i = 1; i <= 20; i++)
                {
                    if (!nums.Contains(i))
                    {
                        ComboBoxItem temp = new ComboBoxItem();
                        temp.Content = $"{i}";
                        temp.Name = $"storeCode{i}";
                        CB_StoreCode.Items.Add(temp);
                    }
                }

                foreach (ComboBoxItem item in CB_StoreCode.Items)
                {
                    if (item.Content.ToString() == $"{GetController().store.Code}")
                    {
                        CB_StoreCode.SelectedValue = item;
                        break;
                    }
                }
            }
        }

        private void EV_StoreName(object sender, RoutedEventArgs e)
        {
            if(TB_StoreName.Text.Length == 0)
            {
                if (SP_StoreName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StoreName.Children.Add(message);
                }

                else if (SP_StoreName.Children.Count == 2)
                {
                    SP_StoreName.Children.RemoveAt(SP_StoreName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StoreName.Children.Add(message);
                }
                GetController().CleanName();
            }

            else if (GetController().CompanyControlExist(TB_StoreName.Text))
            {
                if (SP_StoreName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este almacén ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StoreName.Children.Add(message);
                }

                else if (SP_StoreName.Children.Count == 2)
                {
                    SP_StoreName.Children.RemoveAt(SP_StoreName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este almacén ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StoreName.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_StoreName.Children.Count == 2)
                {
                    SP_StoreName.Children.RemoveAt(SP_StoreName.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_StoreCode.SelectedItem;
            if (temp2 != null)
            {
                GetController().SetStoreCode(Convert.ToInt32(temp2.Name.Replace("storeCode", "")));
            }
        }

        private Controller.CT_STR_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_STR_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_STR_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
