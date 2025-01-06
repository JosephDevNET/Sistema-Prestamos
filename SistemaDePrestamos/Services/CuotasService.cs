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
    public class CuotasService
    {
        public IEnumerable<cuotas> Get()
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.cuotas.ToList();
            }
        }
        public cuotas Get(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.cuotas.FirstOrDefault(x => x.idcuota == id);
            }
        }
        public bool Add(CuotasDTO cuota)
        {
            //if (CuotasValidation.ValidarCuota(cuota))
            //{
                using (DataPrestamos data = new DataPrestamos())
                {
                    data.cuotas.Add(new cuotas
                    {
                        idprestamo = cuota.idprestamo,
                        numero_cuota = cuota.numero_cuota,
                        fecha_vencimiento = cuota.fecha_vencimiento,
                        fecha_pago = cuota.fecha_pago,
                        monto_cuota = cuota.monto_cuota,
                        monto_pagado = cuota.monto_pagado,
                        estado = cuota.estado
                    });
                    data.SaveChanges();
                    return true;
                }
            //}
            //else
            //{
            //    return false;
            //}
        }
        public bool Update(CuotasDTO cuota, int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var cuotaUpdate = data.cuotas.FirstOrDefault(x => x.idcuota == id);
                if (cuotaUpdate == null) return false;
                else
                {
                    cuotaUpdate.numero_cuota = cuota.numero_cuota;
                    cuotaUpdate.fecha_vencimiento = cuota.fecha_vencimiento;
                    cuotaUpdate.fecha_pago = cuota.fecha_pago;
                    cuotaUpdate.monto_cuota = cuota.monto_cuota;
                    cuotaUpdate.monto_pagado = cuota.monto_pagado;
                    cuotaUpdate.estado = cuota.estado;
                    data.SaveChanges();
                    return true;
                }
            }
        }
        public bool Delete(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var cuota = data.cuotas.FirstOrDefault(x => x.idcuota == id);
                if (cuota != null)
                {
                    data.cuotas.Remove(cuota);
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
