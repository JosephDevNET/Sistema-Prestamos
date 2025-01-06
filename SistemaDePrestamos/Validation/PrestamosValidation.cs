using SistemaDePrestamos.Data;
using SistemaDePrestamos.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.Validation
{
    public class PrestamosValidation
    {
        public static bool ValidarPrestamo(PrestamosDTO prestamo)
        {
            using (DataPrestamos context = new DataPrestamos())
            {
                if (context.Prestamos.Any(x => x.id_cliente == prestamo.id_cliente))
                {
                    if (prestamo.monto <= 0) return false;
                    if (prestamo.interes <= 0) return false;
                    if (prestamo.plazo_meses <= 0) return false;
                    if (prestamo.Frecuencia_pagos.ToString() == null) return false;
                    if (prestamo.estado == null) return false;
                    if (prestamo.fecha_inicio == null) return false;
                    if (prestamo.fecha_fin == null) return false;
                    return true;
                }
                else return false;
            }
            
        }
    }
}
