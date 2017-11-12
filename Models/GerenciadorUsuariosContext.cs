namespace GerenciadorUsuarios
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GerenciadorUsuariosContext : DbContext
    {
        public GerenciadorUsuariosContext()
            : base("name=GerenciadorUsuariosContext")
        {
        }

        public virtual DbSet<Direito> Direitos { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Direito>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Direito>()
                .HasMany(e => e.Usuarios)
                .WithMany(e => e.Direitos)
                .Map(m => m.ToTable("Usuario_Direito").MapLeftKey("idDireito").MapRightKey("idUsuario"));

            modelBuilder.Entity<Log>()
                .Property(e => e.Acao)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
