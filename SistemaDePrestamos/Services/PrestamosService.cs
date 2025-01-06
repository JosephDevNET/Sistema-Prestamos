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
    public class PrestamosService
    {
        public IEnumerable<Prestamos> Get()
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.Prestamos.ToList();
            }
        }
        public Prestamos Get(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                return data.Prestamos.FirstOrDefault(x => x.id_prestamo == id);
            }
        }
        public bool Add(PrestamosDTO prestamo)
        {
            //if (PrestamosValidation.ValidarPrestamo(prestamo))
            {
                using (DataPrestamos data = new DataPrestamos())
                {
                    data.Prestamos.Add(new Prestamos
                    {
                        id_cliente = prestamo.id_cliente,
                        monto = prestamo.monto,
                        interes = prestamo.interes,
                        plazo_meses = prestamo.plazo_meses,
                        Frecuencia_pagos = prestamo.Frecuencia_pagos.ToString(),
                        estado = prestamo.estado,
                        fecha_inicio = prestamo.fecha_inicio,
                        fecha_fin = prestamo.fecha_fin
                    });
                    data.SaveChanges();
                    return true;
                }
                //}
                //else
                //{
                //    return false;
                //
            }
        }
        public bool Update(PrestamosDTO prestamo, int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var prestamoUpdate = data.Prestamos.FirstOrDefault(x => x.id_prestamo == id);
                if (prestamoUpdate != null) return false;
                else
                {
                    prestamoUpdate.id_cliente = prestamo.id_cliente;
                    prestamoUpdate.monto = prestamo.monto;
                    prestamoUpdate.interes = prestamo.interes;
                    prestamoUpdate.plazo_meses = prestamo.plazo_meses;
                    prestamoUpdate.Frecuencia_pagos = prestamo.Frecuencia_pagos.ToString();
                    prestamoUpdate.estado = prestamo.estado;
                    prestamoUpdate.fecha_inicio = prestamo.fecha_inicio;
                    prestamoUpdate.fecha_fin = prestamo.fecha_fin;
                    data.SaveChanges();
                    return true;
                }
            }
        }
        public bool Delete(int id)
        {
            using (DataPrestamos data = new DataPrestamos())
            {
                var prestamo = data.Prestamos.FirstOrDefault(x => x.id_prestamo == id);
                if (prestamo != null)
                {
                    data.Prestamos.Remove(prestamo);
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
