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

namespace GestCloudv2.Files.Nodes.Entities.View
{
    /// <summary>
    /// Interaction logic for MC_Entity_Loaded.xaml
    /// </summary>
    public partial class MC_Entity_Loaded : Page
    {
        public MC_Entity_Loaded()
        { 
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_Entity_Name.Text = GetController().entity.Name;
            TB_Entity_SubName.Text = GetController().entity.Subname;
            TB_Entity_Phone.Text = GetController().entity.Phone1;
            TB_Entity_NIF.Text = GetController().entity.NIF;
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
