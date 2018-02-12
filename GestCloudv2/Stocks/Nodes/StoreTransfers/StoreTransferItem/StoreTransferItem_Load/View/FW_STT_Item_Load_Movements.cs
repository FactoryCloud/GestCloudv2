using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferItem.StoreTransferItem_Load.View
{
    public partial class FW_STT_Item_Load_Movements : FloatWindows.ProductSelectWindow
    {
        public FW_STT_Item_Load_Movements() : base()
        {

        }

        public FW_STT_Item_Load_Movements(Movement mov) : base(mov)
        {

        }

        public FW_STT_Item_Load_Movements(int OperationType, List<Movement> Movements) : base(OperationType, Movements)
        {

        }

        public FW_STT_Item_Load_Movements(int OperationType, List<Movement> Movements, Movement movement) : base(OperationType, Movements, movement)
        {

        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STT_Item_Load)a.MainFrame.Content;
        }
    }
}
