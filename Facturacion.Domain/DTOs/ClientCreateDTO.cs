using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class ClientCreateDTO
    {
        [Required(ErrorMessage ="El {0} es obligatorio")]
        
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(100)]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        public string Dni { get; set; }

        [JsonIgnore]
        public bool? Status { get; set; } = true;
    }
}
