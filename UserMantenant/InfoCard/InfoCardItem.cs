using FrameworkDB.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class InfoCardItem
    {
        public string Name { get; set; }
        public int Option { get; set; }

        public string Title { get; set; }

        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }

        public InfoCardItem()
        {

        }

        public InfoCardItem(ProductType productType)
        {
            Title = $"Tipo de Producto: {productType.Code} {productType.Name}";
        }

        public InfoCardItem(Store store)
        {
            Title = $"Almacén: {store.Code} {store.Name}";
        }

        public InfoCardItem(Client client)
        {
            Title = $"Cliente: {client.Code} {client.entity.Name}";
            Content1 = $"Nombre: {client.entity.Name} ({client.entity.Subname})";
            Content2 = $"NIF : {client.entity.NIF}";
            Content3 = $"Fecha última venta:";
        }

        public InfoCardItem(Provider provider)
        {
            Title = $"Proveedor: {provider.Code} {provider.entity.Name}";
            Content1 = $"Nombre: {provider.entity.Name} ({provider.entity.Subname})";
            Content2 = $"NIF : {provider.entity.NIF}";
            Content3 = $"Fecha última compra:";
        }
    }
}
