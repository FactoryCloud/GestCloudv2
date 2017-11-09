using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.View
{
    public partial class FW_PRO_Item_Load_Entity : FloatWindows.EntitySelectWindow
    {
        public FW_PRO_Item_Load_Entity(int opt)
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
            return (ProviderItem_New.Controller.CT_PRO_Item_New)a.MainFrame.Content;
        }
    }
}
