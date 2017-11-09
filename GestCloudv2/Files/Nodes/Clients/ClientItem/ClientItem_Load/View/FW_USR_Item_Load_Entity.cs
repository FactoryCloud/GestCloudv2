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
using FrameworkView.V1;

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_Load.View
{
    /// <summary>
    /// Interaction logic for FW_USR_Item_New_Entity.xaml
    /// </summary>
    public partial class FW_USR_Item_Load_Entity : FloatWindows.EntitySelectWindow
    {
        public FW_USR_Item_Load_Entity(int opt)
        {
            InitializeComponent();

            entitiesView = new EntitiesView(opt);

            DG_Entities.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SelectedChange);
            DG_Entities.MouseDoubleClick += new MouseButtonEventHandler(EV_SelectEntity);

            this.Loaded += new RoutedEventHandler(EV_Start);
            this.Closed += new EventHandler(EV_Close);
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (UserItem_Load.Controller.CT_USR_Item_Load)a.MainFrame.Content;
        }
    }
}
