using FrameworkView.V1;
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

namespace GestCloudv2.Main.Controller
{
    /// <summary>
    /// Interaction logic for CT_Main.xaml
    /// </summary>
    public partial class CT_Main : Main.Controller.CT_Common
    {
        private Page NV_Page;
        private Page TS_Page;
        private Page MC_Page;
        GestCloudDB db;
        public Dictionary<string, int> Information;//Guarda el estado de la aplicacion, para controlar los permisos del usuario
        User user;

        public CT_Main()
        {
            InitializeComponent();

            db = new GestCloudDB();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);
            Information.Add("old_mode", 0);
            Information.Add("selectedUser", 0);
            Information.Add("old_selected", 0);
            Information.Add("controller", 0);
            Information.Add("userModeEditable", 0);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void MD_Change(int i)
        {
            Information["old_mode"] = Information["mode"];
            Information["mode"] = i;
            Information["option"] = i + 1;
            UpdateComponents();
        }

        public void CT_MainBack()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public void CT_Files()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        //Prepara los componentes para que los cargue ChangeEnviroment
        private void UpdateComponents ()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new Main.View.NV_Main();
                    MC_Page = null;
                    TS_Page = null;
                    ChangeEnviroment();
                    break;
            }
        }


        //Actualiza los componentes de la ventana
        private void ChangeEnviroment()
        {
            TopSide.Content = NV_Page;
            MainContent.Content = MC_Page;
            LeftSide.Content= TS_Page;
        }


        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Main.Controller.CT_Main();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Files.Controller.CT_Files();
                    break;
            }
        }
    }
}
