using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class SubmenuItems
    {
        public List<SubmenuItem> GetSubmenuItems (int option)
        {
            switch(option)
            {
                case 1:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_Product",
                            Content = "Producto",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Comercial",
                            Content = "Comercial",
                            Option = 2
                        }
                    };

                case 2:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_ProductType",
                            Content = "Tipos de producto",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Comercial",
                            Content = "Comercial",
                            Option = 2
                        }
                    };

                case 4:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_Store",
                            Content = "Datos Almacén",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Companies",
                            Content = "Compañias",
                            Option = 2
                        }
                    };

                case 5:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_PaymentMethod",
                            Content = "Datos Forma de pago",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Companies",
                            Content = "Compañias",
                            Option = 2
                        }
                    };

                case 6:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_Client",
                            Content = "Datos Cliente",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Entity",
                            Content = "Datos Personales",
                            Option = 2
                        }
                    };

                case 7:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_Provider",
                            Content = "Datos Proveedor",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Entity",
                            Content = "Datos Personales",
                            Option = 2
                        }
                    };
            }

            return null;
        }
    }
}
