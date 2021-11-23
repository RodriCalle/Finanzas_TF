using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas_TF.Models
{
    public class Usuario: IdentityUser
    {
        public string Fullname { set; get; }
    }
}
