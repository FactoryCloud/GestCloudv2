using System;
using System.Collections.Generic;
using System.Data;
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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_New_User.xaml
    /// </summary>
    public partial class MC_CPN_Item_Load_Company_Configuration : Page
    {
        ConfigurationsView view = new ConfigurationsView();
        public MC_CPN_Item_Load_Company_Configuration()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_ConfigurationType.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_ConfigurationValue.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            DG_Configurations.MouseLeftButtonUp += new MouseButtonEventHandler(EV_DG_Selection);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            List<ConfigurationType> configTypes = GetController().GetConfigurationTypes();
            foreach (ConfigurationType item in configTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{item.Name}";
                temp.Name = $"configType{item.ConfigurationTypeID}";
                CB_ConfigurationType.Items.Add(temp);
            }

            UpdateData();

            if (GetController().Information["editable"] == 0)
            {
                CB_ConfigurationValue.Visibility = Visibility.Hidden;
                TB_ConfigurationValue.Visibility = Visibility.Visible;
                BT_ConfigurationApply.Visibility = Visibility.Hidden;
            }
        }        

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp = (ComboBoxItem)CB_ConfigurationType.SelectedItem;
            if (temp != null)
            {
                view.SetConfigType(Convert.ToInt32(temp.Name.Replace("configType", "")));
                UpdateData();
            }
        }

        private void EV_DG_Selection(object sender, MouseButtonEventArgs e)
        {
            int num = DG_Configurations.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Configurations.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetConfiguration(Convert.ToInt32(dr.Row.ItemArray[0].ToString()));
                TB_ConfigurationDescription.Text = GetController().GetConfiguration().Description;
                UpdateValue();
                BT_ConfigurationApply.IsEnabled = true;
                BT_RestoreConfiguration.IsEnabled = true;
            }
        }

        private void EV_ApplyChanges(object sender, RoutedEventArgs e)
        {
            GetController().SetConfigValue(Convert.ToInt32(((ComboBoxItem)CB_ConfigurationValue.SelectedItem).Tag));
            MessageBox.Show($"Cambios aplicados, no olvide guardar sus cambios");
        }

        private void EV_RestoreValue(object sender, RoutedEventArgs e)
        {
            int num = DG_Configurations.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Configurations.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetConfiguration(Convert.ToInt32(dr.Row.ItemArray[0].ToString()));
                TB_ConfigurationDescription.Text = GetController().GetConfiguration().Description;
                UpdateDefaultValue();
                BT_ConfigurationApply.IsEnabled = true;
                BT_RestoreConfiguration.IsEnabled = true;
            }
            GetController().SetConfigValue(Convert.ToInt32(((ComboBoxItem)CB_ConfigurationValue.SelectedItem).Tag));
            MessageBox.Show("Se ha restaurado el valor para esta configuración, no olvide guardar los cambios");
        }

        private void EV_DefaultValues(object sender, RoutedEventArgs e)
        {
            //GetController().SetConfigValue(Convert.ToInt32(((ComboBoxItem)CB_ConfigurationValue.SelectedItem).Tag));
            //GetController().SetDefaultConfig();
        }

        public void UpdateValue()
        {
            foreach (ComboBoxItem item in CB_ConfigurationValue.Items)
            {
                if (Convert.ToInt16(item.Tag) == GetController().GetConfigurationValue())
                {
                    CB_ConfigurationValue.SelectedValue = item;
                    break;
                }
            }

            TB_ConfigurationValue.Text = ((ComboBoxItem)CB_ConfigurationValue.SelectedItem).Content.ToString();
        }

        public void UpdateDefaultValue()
        {
            foreach (ComboBoxItem item in CB_ConfigurationValue.Items)
            {
                if (Convert.ToInt16(item.Tag) == GetController().GetDefaultConfigurationValue())
                {
                    CB_ConfigurationValue.SelectedValue = item;
                    break;
                }
            }
            TB_ConfigurationValue.Text = ((ComboBoxItem)CB_ConfigurationValue.SelectedItem).Content.ToString();
        }

        public void UpdateData()
        {
            DG_Configurations.ItemsSource = null;
            DG_Configurations.ItemsSource = view.GetTable();
        }

        private Controller.CT_CPN_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_Load)a.MainFrame.Content;
        }
    }
}
