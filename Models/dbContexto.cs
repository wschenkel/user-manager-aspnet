using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GerenciadorUsuarios.Models
{
    public class dbContexto : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mapUsuario = modelBuilder.Entity<Usuario>();
            mapUsuario.Property(x => x.IdUsuario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapUsuario.Property(x => x.Nome).IsVariableLength().IsRequired();
            mapUsuario.Property(x => x.Login).IsVariableLength().IsRequired();
            mapUsuario.Property(x => x.Senha).IsVariableLength().IsRequired();
            mapUsuario.Property(x => x.Email).IsVariableLength().IsRequired();
            mapUsuario.HasKey(x => x.IdUsuario);
            mapUsuario.ToTable("Usuario");


            var mapLog = modelBuilder.Entity<Log>();
            mapLog.Property(x => x.IdLog).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapLog.Property(x => x.Acao).IsVariableLength().IsRequired();
            mapLog.Property(x => x.DataAcao);
            mapLog.HasKey(x => x.IdLog);
            mapLog.ToTable("Log");



        }
    }
}