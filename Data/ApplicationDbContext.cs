using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectGarantia.Models;
using System.ComponentModel.DataAnnotations.Schema;
using static ProyectGarantia.Data.ApplicationDbContext;

namespace ProyectGarantia.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext()
        {
        }
        public class Usuario : IdentityUser {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            
            public int AgenciaId { get; set; }
            [ForeignKey("AgenciaId")]
            public virtual Agencia Agencia { get; set; }
        }
        public DbSet<Agencia> Agencias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(entityTypeBuilder =>
            {
                entityTypeBuilder.ToTable("AspNetUsers");
                entityTypeBuilder.Property(u => u.UserName)
                    .HasMaxLength(100)
                    .HasDefaultValue(0);
                entityTypeBuilder.Property(u => u.Nombres)
                    .HasMaxLength(60);
                entityTypeBuilder.Property(u => u.Apellidos)
                    .HasMaxLength(60);
                entityTypeBuilder.Property(u => u.AgenciaId)
                    .HasMaxLength(10); 

                entityTypeBuilder.HasOne(u => u.Agencia)
                    .WithMany()
                    .HasForeignKey(u => u.AgenciaId)
                    .IsRequired();
            });
        }

        private void entityTypeBuilder(EntityTypeBuilder<Usuario> obj)
        {
            throw new NotImplementedException();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<ProyectGarantia.Models.ContadorLotes> ContadorLotes { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Lote> Lote { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Agencia> Agencia { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Cliente> Cliente { get; set; }
        public virtual DbSet<ProyectGarantia.Models.DetalleLote> DetalleLote { get; set; }
        public virtual DbSet<ProyectGarantia.Models.DetalleLoteModelo> DetalleLoteModelo { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Documentacion> Documentacion { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Garantia> Garantia { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Almacen> Almacen { get; set; }
        public virtual DbSet<ProyectGarantia.Models.Documento> Documento { get; set; }
    }
}