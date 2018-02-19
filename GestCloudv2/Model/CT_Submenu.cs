using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Main.Controller;

namespace GestCloudv2.Model
{
    public class CT_Submenu
    {
        public List<SubmenuItem> items;
        public int buttons;
        public CT_Common Subcontroller;
        public string Name;
        public InfoCardItem infoCardItem;

        public CT_Submenu(object Component, int option)
        {
            switch (option)
            {
                case 2:
                    Subcontroller = new Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_Load.Controller.CT_PTY_Item_Load((ProductType)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Tipo de producto";
                    infoCardItem = new InfoCardItem((ProductType)Component);
                    break;

                case 4:
                    Subcontroller = new Files.Nodes.Stores.StoreItem.StoreItem_Load.Controller.CT_STR_Item_Load((Store)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Almacén";
                    infoCardItem = new InfoCardItem((Store)Component);
                    break;

                case 5:
                    Subcontroller = new Files.Nodes.PaymentMethods.PaymentMethodItem.PaymentMethodItem_Load.Controller.CT_PMT_Item_Load((PaymentMethod)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Forma de pago";
                    infoCardItem = new InfoCardItem((PaymentMethod)Component);
                    break;

                case 6:
                    Subcontroller = new Files.Nodes.Clients.ClientItem.ClientItem_Load.Controller.CT_CLI_Item_Load((Client)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Cliente";
                    infoCardItem = new InfoCardItem((Client)Component);
                    break;

                case 7:
                    Subcontroller = new Files.Nodes.Providers.ProviderItem.ProviderItem_Load.Controller.CT_PRO_Item_Load((Provider)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Proveedor";
                    infoCardItem = new InfoCardItem((Provider)Component);
                    break;
            }
        }
    }
}
