using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePrestamos.DTOS
{
    public enum EFrecuencia
    {
        Diario,
        Semanal,
        Quincenal,
        Mensual,
        Anual
    }
    public class PrestamosDTO
    {
        public int id_cliente { get; set; }
        public float monto { get; set; }
        public float interes { get; set; }
        public int plazo_meses { get; set; }
        public EFrecuencia Frecuencia_pagos { get; set; }
        public string estado { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
    }
}
