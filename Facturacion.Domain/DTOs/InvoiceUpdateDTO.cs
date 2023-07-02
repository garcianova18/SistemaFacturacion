using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class InvoiceUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public int IdClient { get; set; }

        public ICollection<InvoiceDetailsUpdateDTO> InvoiceDetails { get; set; }

    }
 
}
