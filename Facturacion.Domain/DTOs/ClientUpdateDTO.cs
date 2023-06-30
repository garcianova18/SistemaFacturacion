using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Facturacion.Domain.DTOs
{
    public class ClientUpdateDTO
    {
        [Required(ErrorMessage ="El {0} Es obligatirio")]
        [Range(1,int.MaxValue, ErrorMessage ="El {0 debe de ser mayor a 0}")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Apellidos")]
        [StringLength(60)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "Direccion")]
        [StringLength(100)]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "DNI")]
        [StringLength(50)]
        public string Dni { get; set; }
    }
}
