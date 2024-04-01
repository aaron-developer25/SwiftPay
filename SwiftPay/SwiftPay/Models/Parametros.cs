using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Models
{
    public class Parametros
    {
        [Key]
        public int ParametroOperatacionalesId { get; set; }

        [Required(ErrorMessage = "El campo Monto Gravedad es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El monto de gravedad no puede ser negativo")]
        public float MontoGravedad { get; set; }

        [Required(ErrorMessage = "El campo Monto Bomba es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El monto de bomba no puede ser negativo")]
        public float MontoBomba { get; set; }

        [Required(ErrorMessage = "El campo Porcentaje de Recargos es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El porcentaje de recargos no puede ser negativo")]
        public float PorcentajeRecargos { get; set; }

        [Required(ErrorMessage = "El campo Tiempo Próximo Pago es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El tiempo para próximo pago no puede ser negativo")]
        public int TiempoProximoPago { get; set; }
    }
}
