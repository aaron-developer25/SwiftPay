using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Models
{
    public class Asociaciones
    {
        [Key]
        public int AsociacionId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ ]*$", ErrorMessage = "El nombre debe comenzar con una letra mayúscula y no debe contener números ni símbolos.")]
        public string? Nombre { get; set; }
    }
}
