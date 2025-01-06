using SistemaDePrestamos.Data;
using SistemaDePrestamos.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.Validation
{
    public class PagosValidation
    {
        public static bool PagoValidation(PagosDTO pagos)
        {
            using (DataPrestamos context = new DataPrestamos())
            {
                if (context.Prestamos.Any(x => x.id_prestamo == pagos.id_prestamo))
                {
                    if (pagos.monto_pago <= 0) return false;
                    if (pagos.fecha_pago == null) return false;
                    return true;
                }
                else return false;
            }
        }
    }
}
