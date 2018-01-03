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
using FrameworkView.V1;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_New.View
{
    /// <summary>
    /// Interaction logic for MC_DCM_Item_New_Main.xaml
    /// </summary>
    public partial class MC_DCM_Item_New_Main : Page
    {

        public MC_DCM_Item_New_Main()
        {

            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_Code.KeyUp += new KeyEventHandler(EV_Code);
            TB_Code.Loaded += new RoutedEventHandler(EV_Code);
            CB_Stores.SelectionChanged += new SelectionChangedEventHandler(EV_StoreSelect);
            DP_Date.KeyDown += new KeyEventHandler(EV_Cancel);
            DP_Date.KeyUp += new KeyEventHandler(EV_Cancel);

            GR_Provider.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Provider.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Provider.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            GR_Store.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Store.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Store.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            SetSelected();
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            DP_Date.SelectedDate = DateTime.Now;
            TB_Code.Text = GetController().GetCode();
            List<Store> stores = GetController().GetStores();
            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"store{st.StoreID}";
                CB_Stores.Items.Add(temp);
            }

            CB_Stores.SelectedIndex = 0;

            if (GetController().provider.entity != null)
            {
                TB_ProviderName.Text = GetController().provider.entity.Name;
                TB_ProviderCode.Text = GetController().provider.ProviderID.ToString();
            }
        }

        private void SetTransparentAll()
        {
            GR_Store.Background = new SolidColorBrush(Colors.Transparent);
            GR_Provider.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void SetSelected()
        {
            switch (GetController().Information["submode"])
            {
                case 4:
                    GR_Store.Background = new SolidColorBrush(Colors.Green);
                    break;

                case 7:
                    GR_Provider.Background = new SolidColorBrush(Colors.Green);
                    break;
            }
        }

        private void EV_MouseChange(object sender, RoutedEventArgs e)
        {
            SetTransparentAll();
            if (GetController().provider.ProviderID > 0)
            {
                if (GR_Provider.IsMouseOver)
                {
                    GR_Provider.Background = new SolidColorBrush(Colors.Red);
                }
            }

            if (GetController().store.StoreID > 0)
            {
                if (GR_Store.IsMouseOver)
                {
                    GR_Store.Background = new SolidColorBrush(Colors.Red);
                }
            }
            SetSelected();
        }

        private void EV_MouseClick(object sender, RoutedEventArgs e)
        {

            if (GetController().provider.ProviderID > 0)
            {
                if (GR_Provider.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(7);
                }
            }

            if (GetController().store.StoreID > 0)
            {
                if (GR_Store.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(4);
                }
            }
            SetTransparentAll();
            SetSelected();
        }

        private void EV_Code(object sender, RoutedEventArgs e)
        {
            if (TB_Code.Text.Length == 0)
            {
                if (SP_Code.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Code.Children.Add(message);
                }

                else if (SP_Code.Children.Count == 2)
                {
                    SP_Code.Children.RemoveAt(SP_Code.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Code.Children.Add(message);
                }
                GetController().CleanCode();
                TB_Code.Text = "";
            }
            else if (TB_Code.Text.Any(x => Char.IsWhiteSpace(x)))
            {
                if (SP_Code.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede contener espacios";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Code.Children.Add(message);
                }

                else if (SP_Code.Children.Count == 2)
                {
                    SP_Code.Children.RemoveAt(SP_Code.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede contener espacios";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Code.Children.Add(message);
                }
                GetController().CleanCode();
            }

             else if (GetController().CodeExist(TB_Code.Text))
             {
                 if (SP_Code.Children.Count == 1)
                 {
                     TextBlock message = new TextBlock();
                     message.TextWrapping = TextWrapping.WrapWithOverflow;
                     message.Text = "Este código ya existe";
                     message.HorizontalAlignment = HorizontalAlignment.Center;
                     SP_Code.Children.Add(message);
                 }

                 else if (SP_Code.Children.Count == 2)
                 {
                     SP_Code.Children.RemoveAt(SP_Code.Children.Count - 1);
                     TextBlock message = new TextBlock();
                     message.TextWrapping = TextWrapping.WrapWithOverflow;
                     message.Text = "Este código ya existe";
                     message.HorizontalAlignment = HorizontalAlignment.Center;
                     SP_Code.Children.Add(message);
                 }
                 GetController().EV_UpdateIfNotEmpty(true);
             }

             else
             {
                 if (SP_Code.Children.Count == 2)
                 {
                     SP_Code.Children.RemoveAt(SP_Code.Children.Count - 1);
                 }
                 GetController().EV_UpdateIfNotEmpty(true);
             }
        }

        private void EV_Cancel(object sender, KeyEventArgs e)
        {
                e.Handled = true;
        }

        private void EV_OrderReference(object sender, RoutedEventArgs e)
        {
        }

        protected void EV_StoreSelect(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Stores.SelectedItem;

            if (CB_Stores.SelectedIndex >= 0)
            {
                GetController().SetStore(Convert.ToInt32(temp1.Name.Replace("store", "")));
            }
        }

        public void EV_DateChange(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;

            DateTime? date = picker.SelectedDate;

            this.Title = date.Value.ToShortDateString();
            GetController().SetDate(date.Value);
        }

        private void EV_ProviderSelect(object sender, RoutedEventArgs e)
        {
            GetController().MD_ProviderSelect();
        }

        virtual public Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Item_New)a.MainFrame.Content;
        }
    }
}
