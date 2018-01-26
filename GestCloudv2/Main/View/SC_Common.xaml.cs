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

namespace GestCloudv2.Main.View
{
    /// <summary>
    /// Interaction logic for SC_Common.xaml
    /// </summary>
    public partial class SC_Common : Page
    {
        public SC_Common()
        {
            InitializeComponent();

            int num = 0;
            foreach(Shortcuts.ShortcutDocument doc in ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).shortcutDocuments)
            {
                StackPanel panel = new StackPanel();
                Label label1 = new Label();
                label1.Content = doc.Name;
                panel.Children.Add(label1);

                Button close = new Button();
                close.Content = "Eliminar Vínculo";
                close.Tag = doc.Id;
                close.Click += new RoutedEventHandler(EV_ShortcutDocumentsUpdate);
                panel.Children.Add(close);

                StackPanel mainPanel = new StackPanel();
                mainPanel.Background = new SolidColorBrush(Colors.Gray);
                mainPanel.Tag = doc.Id;
                mainPanel.MouseDown += new MouseButtonEventHandler (EV_ShortcutDocument);
                Grid.SetRow(mainPanel, num);
                mainPanel.Children.Add(panel);
                GR_ShortcutDocuments.Children.Add(mainPanel);
                num++;
            }

            // InfoCard
            InfoCardItem infoCardItem = ((MainWindow)Application.Current.MainWindow).infoCardItem;
            LB_InfoCardTitle.Content = infoCardItem.Title;
            LB_InfoCardLine1.Content = infoCardItem.Content1;
            LB_InfoCardLine2.Content = infoCardItem.Content2;
            LB_InfoCardLine3.Content = infoCardItem.Content3;
        }

        public virtual void CT_DocumentMinimize(object sender, RoutedEventArgs e)
        {

        }

        private void EV_ShortcutDocumentsUpdate(object sender, RoutedEventArgs e)
        {
            GetController().EV_UpdateShortcutDocuments(Convert.ToInt16(((Button)sender).Tag));
        }

        private void EV_ShortcutDocument(object sender, RoutedEventArgs e)
        {
            GetController().CT_ShortcutDocumentActivate(Convert.ToInt16(((StackPanel)sender).Tag));
        }

        public virtual Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Common)a.MainFrame.Content;
        }
    }
}
