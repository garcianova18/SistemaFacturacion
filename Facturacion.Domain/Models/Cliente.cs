using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Correo { get; set; }
        public string Dni { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
