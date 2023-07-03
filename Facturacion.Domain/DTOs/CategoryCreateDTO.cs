using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class CategoryCreateDTO
    {
        [Required (ErrorMessage ="el {0} es obligatirio")]
        [StringLength(maximumLength: 50, ErrorMessage = "El maximo de caracteres permitidos para {0} es {1}")]
        public string Name { get; set; }

       

    }
}
