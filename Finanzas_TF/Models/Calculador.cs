using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas_TF.Models
{
    public class Calculador
    {
        public int TipoDeTasa { get; set; }
        public decimal Tasa { get; set; }
        public int Anio { get; set; }
        public DateTime FechaDescuento { set; get; }
        public int Moneda { get; set; }
        public decimal Dolar { get; set; }
        public decimal gInicio { set; get; }
        public decimal gFinal { set; get; }
        public decimal ValorARecibir { set; get; }
        public decimal TCEA { set; get; }
        public decimal TEA { set; get; }

    }
}
