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
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Data;
using System.Collections;

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.View
{
    /// <summary>
    /// Interaction logic for NewUser_MainPage.xaml
    /// </summary>
    public partial class MC_CLI_Item_New : Page
    {
        private GestCloudDB db;
        private DataTable dt;

        public MC_CLI_Item_New()
        {
            InitializeComponent();
            dt = new DataTable();
            db = new GestCloudDB();
            this.Loaded += new RoutedEventHandler(StartNewUserMain_Event);
        }

        private void StartNewUserMain_Event(object sender, RoutedEventArgs e)
        {
            TB_Name.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
            TB_Subname.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
            TB_Phone.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
            TB_Email.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
        }

        private void ControlFieldsKey_Event(object sender, RoutedEventArgs e)
        {
            if (TB_Name.Text.Length <= 30 && TB_Subname.Text.Length <= 30 && TB_Phone.Text.Length <= 20 && TB_Email.Text.Length <= 50 && TB_Name.Text.Length > 0 && TB_Subname.Text.Length > 0 && TB_Phone.Text.Length > 0 && TB_Email.Text.Length > 0) 
            {
                GetController().entity = new Entity
                {
                    Name = TB_Name.Text.ToString(),
                    Subname = TB_Subname.Text.ToString(),
                    Phone1 = TB_Phone.Text.ToString(),
                    Email = TB_Email.Text.ToString()
                };
                GetController().ControlFieldChangeButton(true);
            }
            else
            {
                GetController().ControlFieldChangeButton(false);
            }

            if(!string.IsNullOrEmpty(TB_Name.Text.ToString()) || !string.IsNullOrEmpty(TB_Subname.Text.ToString()) || !string.IsNullOrEmpty(TB_Phone.Text.ToString()) || !string.IsNullOrEmpty(TB_Email.Text.ToString()))
            {
                GetController().UpdateIfNotEmpty(true);
            }

            else
            {
                GetController().UpdateIfNotEmpty(false);
            }
        }

        private ClientItem_New.Controller.CT_CLI_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ClientItem_New.Controller.CT_CLI_Item_New)a.MainFrame.Content;
        }
    }
}
