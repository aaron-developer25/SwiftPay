using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Models
{
    public class DetalleRegante
    {
        [Key]
        public int DetalleReganteId { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [ForeignKey("Regante")]
        public int ReganteId { get; set; }

        [Required(ErrorMessage = "El campo Código Parcela es obligatorio")]
        public string? CodigoParcela { get; set; }

        [Required(ErrorMessage = "El campo Tareas es obligatorio")]
        public float Tareas { get; set; }

        [Required(ErrorMessage = "El campo Tipo Irrigación es obligatorio")]
        [RegularExpression("^(Bomba|Gravedad)$", ErrorMessage = "El tipo de irrigación solo puede ser 'bomba' o 'gravedad'")]
        public string? TipoIrrigacion { get; set; }
    }
}
