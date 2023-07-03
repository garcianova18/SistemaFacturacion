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
        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="El {0 debe de ser mayor a 0}")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [StringLength(100)]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        

        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [StringLength(50)]
        public string Dni { get; set; }


        public bool? Status { get; set; } = true;
    }
}
