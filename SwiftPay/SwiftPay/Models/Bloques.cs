using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Models
{
    public class Bloques
    {
        [Key]
        public int BloqueId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Nombre { get; set; }
    }
}
