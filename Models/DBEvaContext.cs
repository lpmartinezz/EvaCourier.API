using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EvaCourier.API.Models
{
    public partial class DBEvaContext : DbContext
    {
        //public DBEvaContext()
        //{
        //}

        public DBEvaContext(DbContextOptions<DBEvaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Opcion> Opcion { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Perfilopcion> Perfilopcion { get; set; }
        public virtual DbSet<Ubigeo> Ubigeo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-5KD39FJ;user=sa;password=Peru+2020;Database=DBEva;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Opcion>(entity =>
            {
                entity.HasKey(e => e.Idopcion)
                    .HasName("XPKOPCION");

                entity.ToTable("OPCION");

                entity.Property(e => e.Idopcion).HasColumnName("IDOPCION");

                entity.Property(e => e.Crea).HasColumnName("CREA");

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.Property(e => e.Fechacrea)
                    .HasColumnName("FECHACREA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechamodifica)
                    .HasColumnName("FECHAMODIFICA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modifica).HasColumnName("MODIFICA");

                entity.Property(e => e.Nombreopcion)
                    .HasColumnName("NOMBREOPCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rutaopcion)
                    .HasColumnName("RUTAOPCION")
                    .HasMaxLength(350)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.Idperfil)
                    .HasName("XPKPERFIL");

                entity.ToTable("PERFIL");

                entity.Property(e => e.Idperfil).HasColumnName("IDPERFIL");

                entity.Property(e => e.Crea).HasColumnName("CREA");

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.Property(e => e.Fechacrea)
                    .HasColumnName("FECHACREA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechamodifica)
                    .HasColumnName("FECHAMODIFICA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modifica).HasColumnName("MODIFICA");

                entity.Property(e => e.Nombreperfil)
                    .HasColumnName("NOMBREPERFIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Perfilopcion>(entity =>
            {
                entity.HasKey(e => e.Idperfilopcion)
                    .HasName("XPKPERFILOPCION");

                entity.ToTable("PERFILOPCION");

                entity.Property(e => e.Idperfilopcion).HasColumnName("IDPERFILOPCION");

                entity.Property(e => e.Crea).HasColumnName("CREA");

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.Property(e => e.Fechacrea)
                    .HasColumnName("FECHACREA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechamodifica)
                    .HasColumnName("FECHAMODIFICA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idopcion).HasColumnName("IDOPCION");

                entity.Property(e => e.Idperfil).HasColumnName("IDPERFIL");

                entity.Property(e => e.Modifica).HasColumnName("MODIFICA");

                entity.HasOne(d => d.IdopcionNavigation)
                    .WithMany(p => p.Perfilopcion)
                    .HasForeignKey(d => d.Idopcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERFILOPCION_OPCION");

                entity.HasOne(d => d.IdperfilNavigation)
                    .WithMany(p => p.Perfilopcion)
                    .HasForeignKey(d => d.Idperfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERFILOPCION_PERFIL");
            });

            modelBuilder.Entity<Ubigeo>(entity =>
            {
                entity.HasKey(e => e.Idubigeo)
                    .HasName("XPKUBIGEO");

                entity.ToTable("UBIGEO");

                entity.Property(e => e.Idubigeo).HasColumnName("IDUBIGEO");

                entity.Property(e => e.Codigoubigeo)
                    .HasColumnName("CODIGOUBIGEO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Crea).HasColumnName("CREA");

                entity.Property(e => e.Departamento)
                    .HasColumnName("DEPARTAMENTO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Distrito)
                    .HasColumnName("DISTRITO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.Property(e => e.Fechacrea)
                    .HasColumnName("FECHACREA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechamodifica)
                    .HasColumnName("FECHAMODIFICA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modifica).HasColumnName("MODIFICA");

                entity.Property(e => e.Provincia)
                    .HasColumnName("PROVINCIA")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("XPKUSUARIO");

                entity.ToTable("USUARIO");

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("APELLIDOS")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Bloqueado).HasColumnName("BLOQUEADO");

                entity.Property(e => e.Celular01)
                    .IsRequired()
                    .HasColumnName("CELULAR01")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Celular02)
                    .HasColumnName("CELULAR02")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("CLAVE")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("CORREO")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Correo2)
                    .IsRequired()
                    .HasColumnName("CORREO2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Crea).HasColumnName("CREA");

                entity.Property(e => e.Direccion01)
                    .IsRequired()
                    .HasColumnName("DIRECCION01")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion02)
                    .HasColumnName("DIRECCION02")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.Property(e => e.Fechacrea)
                    .HasColumnName("FECHACREA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechamodifica)
                    .HasColumnName("FECHAMODIFICA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idperfil).HasColumnName("IDPERFIL");

                entity.Property(e => e.Idubigeo).HasColumnName("IDUBIGEO");

                entity.Property(e => e.Intentos).HasColumnName("INTENTOS");

                entity.Property(e => e.Modifica).HasColumnName("MODIFICA");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("NOMBRES")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Nombreusuario)
                    .IsRequired()
                    .HasColumnName("NOMBREUSUARIO")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono01)
                    .HasColumnName("TELEFONO01")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono02)
                    .HasColumnName("TELEFONO02")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ubicacion01)
                    .IsRequired()
                    .HasColumnName("UBICACION01")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Ubicacion02)
                    .HasColumnName("UBICACION02")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdperfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Idperfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_PERFIL");

                entity.HasOne(d => d.IdubigeoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Idubigeo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_UBIGEO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
