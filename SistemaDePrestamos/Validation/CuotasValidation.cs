using SistemaDePrestamos.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.Validation
{
    public class CuotasValidation
    {
        public static bool ValidarCuota(CuotasDTO cuota)
        {
            if (cuota.idprestamo <= 0) return false;
            if (cuota.numero_cuota <= 0) return false;
            if (cuota.fecha_vencimiento == null) return false;
            if (cuota.monto_cuota <= 0) return false;
            if (cuota.estado == null) return false;
            return true;
        }
    }
}
