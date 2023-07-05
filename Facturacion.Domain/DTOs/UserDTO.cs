using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        [EmailAddress (ErrorMessage ="El email no oes valido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="La password es obligatoria")]
        public string Password { get; set; }
    }
}
