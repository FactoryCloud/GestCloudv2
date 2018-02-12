using FrameworkDB.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferItem.StoreTransferItem_New.View
{
    class MC_STT_Item_New_StoreTransfer : Documents.DCM_Items.DCM_Item_New.View.MC_DCM_Item_New_Main
    {
        public MC_STT_Item_New_StoreTransfer()
        {
            CB_StoresFrom.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Stores);
            CB_StoresTo.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Stores);

            if(GetController().movements.Count > 0)
            {
                CB_StoresFrom.IsEnabled = false;
                CB_StoresTo.IsEnabled = false;
            }

            GR_Store.Visibility = Visibility.Hidden;
            GR_StoreFrom.Visibility = Visibility.Visible;
            GR_StoreTo.Visibility = Visibility.Visible;
        }

        public override void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);

            List<Store> stores = GetController().GetStores();
            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"storeFrom{st.StoreID}";
                temp.Tag = "storeFrom";
                CB_StoresFrom.Items.Add(temp);

                ComboBoxItem temp2 = new ComboBoxItem();
                temp2.Content = $"{st.Code} - {st.Name}";
                temp2.Name = $"storeTo{st.StoreID}";
                temp.Tag = "storeTo";
                CB_StoresTo.Items.Add(temp2);
            }

            foreach (ComboBoxItem item in CB_StoresFrom.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("storeFrom", "")) == GetController().GetStoreFrom().StoreID)
                {
                    CB_StoresFrom.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_StoresTo.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("storeTo", "")) == GetController().GetStoreTo().StoreID)
                {
                    CB_StoresTo.SelectedValue = item;
                    break;
                }
            }
        }

        private void EV_CB_Stores(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)CB_StoresFrom.SelectedItem) != null && ((ComboBoxItem)CB_StoresTo.SelectedItem) != null)
            {
                if (Convert.ToInt32(((ComboBoxItem)CB_StoresFrom.SelectedItem).Name.Replace("storeFrom", "")) == Convert.ToInt32(((ComboBoxItem)CB_StoresTo.SelectedItem).Name.Replace("storeTo", "")))
                {
                    EV_SwapStores();

                    foreach (ComboBoxItem item in CB_StoresFrom.Items)
                    {
                        if (Convert.ToInt16(item.Name.Replace("storeFrom", "")) == GetController().GetStoreFrom().StoreID)
                        {
                            CB_StoresFrom.SelectedValue = item;
                            break;
                        }
                    }

                    foreach (ComboBoxItem item in CB_StoresTo.Items)
                    {
                        if (Convert.ToInt16(item.Name.Replace("storeTo", "")) == GetController().GetStoreTo().StoreID)
                        {
                            CB_StoresTo.SelectedValue = item;
                            break;
                        }
                    }
                }

                else
                {
                    switch (((ComboBox)sender).Tag)
                    {
                        case "storeFrom":
                            GetController().SetStoreFrom(Convert.ToInt32(((ComboBoxItem)CB_StoresFrom.SelectedItem).Name.Replace("storeFrom", "")));
                            break;

                        case "storeTo":
                            GetController().SetStoreTo(Convert.ToInt32(((ComboBoxItem)CB_StoresTo.SelectedItem).Name.Replace("storeTo", "")));
                            break;
                    }
                }
            }
        }

        private void EV_SwapStores()
        {
            Store temp = GetController().GetStoreFrom();
            GetController().SetStoreFrom(GetController().GetStoreTo().StoreID);
            GetController().SetStoreTo(temp.StoreID);
        }

        override public Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STT_Item_New)a.MainFrame.Content;
        }
    }
}
