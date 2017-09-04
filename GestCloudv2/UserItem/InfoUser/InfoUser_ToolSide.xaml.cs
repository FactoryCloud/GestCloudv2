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
        public InfoUser_ToolSide(bool editable)
        {
            InitializeComponent();
            if(editable)
            {
                ChangeToEdit(true);
            }
        }

        private void AccessInfoUserEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangeMode(1);

        }

        private void EditInfoUserEvent(object sender, RoutedEventArgs e)
        {
            ChangeToEdit(true);
        }

        public void ChangeToEdit(bool editable)
        {
            EditButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            if(!editable)
            {
                GetController().ChangeEditable(Convert.ToInt32(editable));
            } 
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
