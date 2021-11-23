using Finanzas_TF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finanzas_TF.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ReciboHonorarios> ReciboHonorarios { set; get; }
        public DbSet<Cliente> Clientes { set; get; }
    }
}
