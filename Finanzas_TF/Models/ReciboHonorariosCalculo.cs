using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas_TF.Models
{
    public class ReciboHonorariosCalculo
    { 
        public string NombreCliente { set; get; }
        public decimal Monto { set; get; }
        public DateTime FechaPago { set; get; }
        public int dias { set; get; }
        public decimal TEP { set; get; }
        public decimal d { set; get; }
        public decimal Descuento { set; get; }
        public decimal gInicial { set; get; }
        public decimal gFinal { set; get; }
        public decimal ValorNeto { set; get; }
        public decimal ValorRecibir { set; get; }
        public decimal Flujo { set; get; }
        public decimal TCEAUnit { set; get; }
    }
}
