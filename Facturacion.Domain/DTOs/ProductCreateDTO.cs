using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        public string Descriccion { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int IdCategory { get; set; }
        [Required]
        public int Stock { get; set; }
   
    }
}

