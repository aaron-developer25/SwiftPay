using System.ComponentModel.DataAnnotations;

namespace SwiftPay.Models
{
	public class Pagos
	{
        [Key]
        public int PagoId { get; set; }

        [Required(ErrorMessage = "El campo ReganteId es obligatorio")]
        public int ReganteId { get; set; }

        [Required(ErrorMessage = "El campo Código es obligatorio")]
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo Método de Pago es obligatorio")]
        [RegularExpression("^(Tarjeta|Efectivo)$", ErrorMessage = "El método de pago solo puede ser 'Tarjeta' o 'Efectivo'")]
        public string? MetodoPago { get; set; }

        [Required(ErrorMessage = "El campo Monto Pagado es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El monto pagado no puede ser negativo")]
        public float MontoPagado { get; set; }

        [Required(ErrorMessage = "El campo Devuelta es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "La devolución no puede ser negativa")]
        public float Devuelta { get; set; }

        [Required(ErrorMessage = "El campo Recargos es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "Los recargos no pueden ser negativos")]
        public float Recargos { get; set; }

        [Required(ErrorMessage = "El campo Suplementaria es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "La suplementaria no puede ser negativa")]
        public float Suplementaria { get; set; }

        [Required(ErrorMessage = "El campo SubTotal es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El subtotal no puede ser negativo")]
        public float SubTotal { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public string? Estado { get; set; }
    }
}
