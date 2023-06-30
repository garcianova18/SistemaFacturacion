using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descriccion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
