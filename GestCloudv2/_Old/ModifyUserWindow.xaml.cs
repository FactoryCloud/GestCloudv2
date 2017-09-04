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
using System.Windows.Shapes;
using FrameworkDB.V1;
using System.Data;
using FrameworkView.V1;

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for ModifyUserWindow.xaml
    /// </summary>
    public partial class ModifyUserWindow : Window
    {
        UserView userView;
        public event EventHandler UpdateDataEvent;
        private int UpdateFlag;

        public ModifyUserWindow(int userID)
        {
            InitializeComponent();
            UpdateFlag = 0;
            /*userView = new UserView(userID);

            firsnameText.Text = userView.user.FirstName;
            lastnameText.Text = userView.user.LastName;
            usernameText.Text = userView.user.Username;*/
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Datos guardados correctamente");

            UpdateData();
        }

        private void BacktoMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void UpdateData()
        {
            UpdateFlag++;
            if (UpdateFlag >= 1)
            {
                if (this.UpdateDataEvent != null)
                {
                    this.UpdateDataEvent(this, EventArgs.Empty);
                }
            }
        }
    }
}
