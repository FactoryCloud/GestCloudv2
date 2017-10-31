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

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for InfoUser_ToolSide.xaml
    /// </summary>
    public partial class InfoUser_ToolSide : Page
    {

        public InfoUser_ToolSide()
        {
            InitializeComponent();
            if(GetController().Information["editable"] == 1)
            {
                EditButton.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
            }

        }

        private void AccessInfoUserEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangeMode(1);
        }

        private void EditInfoUserEvent(object sender, RoutedEventArgs e)
        {
            ChangeToEdit();
        }

        private void SaveInfoUserEvent(object sender, RoutedEventArgs e)
        {
            GetController().user.FirstName.ToString();
            GetController().user.LastName.ToString();
            GetController().user.Username.ToString();
            GetController().SaveUserChange();
        }

        public void ChangeToEdit()
        {
            EditButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            GetController().ChangeEditable(1);
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MC_Main)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
