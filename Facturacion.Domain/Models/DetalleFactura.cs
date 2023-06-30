using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }

        public virtual Factura IdFacturaNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
