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
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;

            a.changeLeftSide(new UserItem.InfoUser.AccessUser_ToolSide());
            a.changeMainContent(new UserItem.InfoUser.AccessUser_MainContent());
        }

        private void EditInfoUserEvent(object sender, RoutedEventArgs e)
        {
            ChangeToEdit(false);
        }

        public void ChangeToEdit(bool editable)
        {
            EditButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            if(!editable)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (MainWindow)mainWindow;
                var b = (InfoUser_MainContent)a.MainContent.Content;
                b.ChangeToEdit();
            } 
        }
    }
}
