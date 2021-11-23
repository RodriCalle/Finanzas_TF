using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas_TF.Models
{
    public class ReciboHonorarios
    { 
        public Guid Id { set; get; }
        public decimal MontoInicial { set; get; }
        public decimal Retenido { set; get; }
        public decimal Monto { set; get; }
        public int Moneda { set; get; }
        public string Descripcion { set; get; }
        [ForeignKey("Cliente")]
        public Guid IdCliente { set; get; }
        public Cliente Cliente { set; get; }
        public DateTime FechaEmision { set; get; }
        public DateTime FechaPago { set; get; }
    }
}
