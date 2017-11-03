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
using System.Data;
using FrameworkView.V1;

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for InfoUser_MainContent.xaml
    /// </summary>
    public partial class InfoUser_MainContent : Page
    {
        public InfoUser_MainContent()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartInfoUser);
            firsnameText.KeyUp += new KeyEventHandler(EV_TXChanges);
            lastnameText.KeyUp += new KeyEventHandler(EV_TXChanges);
            usernameText.KeyUp += new KeyEventHandler(EV_TXChanges);
        }

        private void StartInfoUser(object sender, RoutedEventArgs e)
        {
            /*firsnameText.Text = GetController().user.FirstName;
            lastnameText.Text = GetController().user.LastName;
            usernameText.Text = GetController().user.Username;*/

            if (GetController().Information["editable"] == 1)
            {
                //MessageBox.Show("modo editable");
                firsnameText.IsReadOnly = false;
                lastnameText.IsReadOnly = false;
                usernameText.IsReadOnly = false;
            }
        }

        private void EV_TXChanges(object sender, RoutedEventArgs e)
        {
            GetController().user.entity.Name = firsnameText.Text;
            GetController().user.entity.Subname = lastnameText.Text;
            GetController().user.Username = usernameText.Text;
            GetController().ControlChanges();
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainFrame.Content;
        }
    }
}
