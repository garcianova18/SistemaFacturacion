using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class InvoiceUpdateDTO
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "el {0 } no puede tener valor 0")]
        public int Id { get; set; }

        [Required (ErrorMessage ="El Numero de la Factura es obligatiro")]
        public string Ninvoice { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "el {0 } no puede tener valor 0")]
        public int IdClient { get; set; }

        [Required]
        [MinLength(1,ErrorMessage ="Debes enviar por lo menos un detalle de ventas")]
        public ICollection<InvoiceDetailsUpdateDTO> InvoiceDetails { get; set; }

        [JsonIgnore]
        public int IdUser { get; set; } = 1;

    }
 
}
