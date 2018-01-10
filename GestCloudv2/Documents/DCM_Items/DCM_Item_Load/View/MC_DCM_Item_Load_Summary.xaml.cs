using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_Load.View
{
    /// <summary>
    /// Interaction logic for MC_DCM_Item_Load_Summary.xaml
    /// </summary>
    public partial class MC_DCM_Item_Load_Summary : Page
    {
        public MC_DCM_Item_Load_Summary()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);

            BT_Provider.Click += new RoutedEventHandler(EV_PopClick);
            BT_Store.Click += new RoutedEventHandler(EV_PopClick);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            InitializingProvider();
            InitializingStore();
        }

        public void InitializingProvider()
        {
            SP_Provider.Width = BT_Provider.ActualWidth;
        }

        public void InitializingStore()
        {
            SP_Store.Width = BT_Store.ActualWidth;
        }

        public void EV_PopClick(object sender, RoutedEventArgs e)
        {
            if(Convert.ToBoolean((sender as ToggleButton).IsChecked))
            {
                switch(Convert.ToInt16((sender as ToggleButton).Tag))
                {
                    case 1:
                        ((Border)BT_Provider.Template.FindName("BR_Provider", BT_Provider)).CornerRadius = new CornerRadius(10, 10, 0, 0);
                        break;

                    case 2:
                        ((Border)BT_Store.Template.FindName("BR_Store", BT_Store)).CornerRadius = new CornerRadius(10, 10, 0, 0);
                        break;
                }
            }

            else
            {
                switch (Convert.ToInt16((sender as ToggleButton).Tag))
                {
                    case 1:
                        ((Border)BT_Provider.Template.FindName("BR_Provider", BT_Provider)).CornerRadius = new CornerRadius(10);
                        break;

                    case 2:
                        ((Border)BT_Store.Template.FindName("BR_Store", BT_Store)).CornerRadius = new CornerRadius(10);
                        break;
                }
            }
        }

        virtual public Controller.CT_DCM_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Item_Load)a.MainFrame.Content;
        }
    }
}
