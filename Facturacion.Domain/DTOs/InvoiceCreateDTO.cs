
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facturacion.Domain.DTOs
{
    public class InvoiceCreateDTO
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "el cliente no puede tener valor 0")]
        public int IdClient { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MinLength(1,ErrorMessage = "Debes enviar por lo menos un detalle de ventas")]
        public  ICollection<InvoiceDetailsCreateDTO> InvoiceDetails { get; set; }
    }

  
}
