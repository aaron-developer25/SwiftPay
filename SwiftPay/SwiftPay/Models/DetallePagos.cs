using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPay.Models
{
	public class DetallePagos
	{
        [Key]
        public int DetallePagoId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [ForeignKey("Pago")]
        public int PagoId { get; set; }

        [Required(ErrorMessage = "El campo Código Parcela es obligatorio")]
        public string? CodigoParcela { get; set; }

        [Required(ErrorMessage = "El campo Tareas es obligatorio")]
        public float Tareas { get; set; }

        [RegularExpression("^(Bomba|Gravedad)$", ErrorMessage = "El tipo de irrigación solo puede ser 'bomba' o 'gravedad'")]
        public string? TipoIrrigacion { get; set; }

        [Required(ErrorMessage = "El campo Periodos es obligatorio")]
        public int Periodos { get; set; }
    }
}
