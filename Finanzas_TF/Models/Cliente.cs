using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas_TF.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { set; get; }
        [Required]
        public string RazonSocial { set; get; }
        [StringLength(450)]
        [Required]
        public string RUC { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Telefono { set; get; }
        public List<ReciboHonorarios> Recibos { set; get; }
        public string Direccion { set; get; }
    }
}
