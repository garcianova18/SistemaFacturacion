using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class ProductDTO
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public decimal Precio { get; set; }
        public int IdCategory { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
