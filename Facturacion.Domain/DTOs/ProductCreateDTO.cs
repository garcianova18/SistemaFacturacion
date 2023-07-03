using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "El maximo de caracteres permitidos para {0} es {1}")]
        public string Name { get; set; } 
        public string Descriccion { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = " {0} debe de ser mayor a 0}")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = " {0} debe de ser mayor a 0}")]
        public int IdCategory { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = " {0} debe de ser mayor a 0}")]
        public int Stock { get; set; }

        [JsonIgnore]
        public bool? Status { get; set; } = true;

    }
}

