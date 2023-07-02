
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [MinLength(1,ErrorMessage ="no se puede crear una factura sin detalles ")]
        public  ICollection<InvoiceDetailsCreateDTO> InvoiceDetails { get; set; }
    }

  
}
