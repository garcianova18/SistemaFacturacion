using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellidos { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Correo { get; set; }
        public string Dni { get; set; }

    }
}
