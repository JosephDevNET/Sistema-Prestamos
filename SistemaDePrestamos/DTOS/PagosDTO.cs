using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.DTOS
{
    public class PagosDTO
    {
        public int id_prestamo { get; set; }
        public float monto_pago { get; set; }
        public DateTime fecha_pago { get; set; }
    }
}
