using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Registro_Inscripcion.Entidades;

namespace Registro_Inscripcion.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Inscripciones> Inscripciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-575M417\SQLEXPRESS; Database = InscripcionDb; Trusted_Connection = True; ");
        }
    }
}
