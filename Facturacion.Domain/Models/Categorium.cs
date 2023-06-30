using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
