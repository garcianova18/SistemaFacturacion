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
        [Required (ErrorMessage ="el Nombre es obligatirio")]
        public string Name { get; set; }

       

    }
}
