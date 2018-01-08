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

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_Load.View
{
    /// <summary>
    /// Interaction logic for TS_DCM_Item_Load_Main.xaml
    /// </summary>
    public partial class TS_DCM_Item_Load_Main : Page
    {
        public TS_DCM_Item_Load_Main()
        {
            InitializeComponent();

            if (GetController().Information["editable"] == 0)
            {
                BT_Save.Visibility = Visibility.Hidden;
            }

            if (GetController().Information["mode"] == 1 || GetController().Information["editable"] == 0)
            {
                BT_MovementAdd.Visibility = Visibility.Hidden;
                BT_MovementDelete.Visibility = Visibility.Hidden;
                BT_MovementEdit.Visibility = Visibility.Hidden;
            }

            if (GetController().movementSelected != null)
            {
                BT_MovementDelete.IsEnabled = true;
                if (GetController().movementSelected.documentType == null)
                {
                    BT_MovementEdit.IsEnabled = true;
                }
            }

            if ((GetController().movements.Count > 0 || GetController().movementsOld.Count > 0) && GetController().Information["minimalInformation"] == 1)
            {
                BT_Save.IsEnabled = true;
            }
        }

        private void EV_MovementAdd(object sender, RoutedEventArgs e)
        {
            GetController().MD_MovementAdd();
        }

        private void EV_MovementEdit(object sender, RoutedEventArgs e)
        {
            GetController().MD_MovementEdit();
        }

        private void EV_MovementDelete(object sender, RoutedEventArgs e)
        {
            GetController().MD_MovementDelete();
        }

        private void EV_Save(object sender, RoutedEventArgs e)
        {
            GetController().SaveDocument();
        }

        virtual public Controller.CT_DCM_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Item_Load)a.MainFrame.Content;
        }
    }
}
