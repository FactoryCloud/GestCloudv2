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
    /// Interaction logic for MC_Entity_Load_Select.xaml
    /// </summary>
    public partial class MC_Entity_Load_Select : Page
    {
        public MC_Entity_Load_Select()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {

        }

        private void EV_EntityEdit(object sender, RoutedEventArgs e)
        {
            GetController().MD_EntityEdit();
        }

        private void EV_EntityLoad(object sender, RoutedEventArgs e)
        {
            GetController().MD_EntityLoad();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
