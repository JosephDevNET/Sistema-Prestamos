using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.DTOS
{
    public class CuotasDTO
    {
        public int idprestamo { get; set; }
        public int numero_cuota { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public DateTime fecha_pago { get; set; }
        public decimal monto_cuota { get; set; }
        public decimal monto_pagado { get; set; }
        public string estado { get; set; }
    }
}
