using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int Id { get; set; }
        public string Nfactura { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Itbis { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
