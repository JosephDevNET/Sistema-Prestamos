using SistemaDePrestamos.Data;
using SistemaDePrestamos.DTOS;
using SistemaDePrestamos.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.Services
{
    internal class PagosService
    {
        public IEnumerable<Pagos> Get()
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.Pagos.ToList();
            }
        }
        public Pagos Get(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.Pagos.FirstOrDefault(x => x.id_pago == id);
            }
        }

        public bool Add(PagosDTO pagos)
        {
            if (PagosValidation.PagoValidation(pagos))
            {
                using (DataPrestamos data = new DataPrestamos())
                {
                    data.Pagos.Add(new Pagos
                    {
                        id_prestamo = pagos.id_prestamo,
                        monto_pago = pagos.monto_pago,
                        fecha_pago = pagos.fecha_pago
                    });
                    data.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Update(PagosDTO pagos, int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var pagosUpdate = data.Pagos.FirstOrDefault(x => x.id_pago == id);
                if (pagosUpdate != null) return false;
                else
                {
                    pagosUpdate.id_prestamo = pagos.id_prestamo;
                    pagosUpdate.monto_pago = pagos.monto_pago;
                    pagosUpdate.fecha_pago = pagos.fecha_pago;
                    data.SaveChanges();
                    return true;
                }
            }
        }
        public bool Delete(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var pagos = data.Pagos.FirstOrDefault(x => x.id_pago == id);
                if (pagos != null)
                {
                    data.Pagos.Remove(pagos);
                    data.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
