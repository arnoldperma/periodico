using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace programfin.Models
{
    public partial class PeriodicoContext : DbContext
    {
        public PeriodicoContext()
        {
        }

        public PeriodicoContext(DbContextOptions<PeriodicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Noticium> Noticia { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=DESKTOP-OGLRV5O; database=Periodico; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A10CA4AC25E");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Noticium>(entity =>
            {
                entity.HasKey(e => e.IdNoticia)
                    .HasName("PK__Noticia__A6C949A80E690FAD");

                entity.Property(e => e.Cuerpo)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_registro");

                entity.Property(e => e.FotoPortada).HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Noticia_Categoria");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Noticia_Usuario");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C27806F5C");

                entity.ToTable("Rol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF9758C35872");

                entity.ToTable("Usuario");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_registro");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Usuario");

                entity.HasMany(d => d.IdRols)
                    .WithMany(p => p.IdUsuarios)
                    .UsingEntity<Dictionary<string, object>>(
                        "Usuariorol",
                        l => l.HasOne<Rol>().WithMany().HasForeignKey("IdRol").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_usuariorol_Rol"),
                        r => r.HasOne<Usuario>().WithMany().HasForeignKey("IdUsuario").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_usuariorol_Usuario"),
                        j =>
                        {
                            j.HasKey("IdUsuario", "IdRol");

                            j.ToTable("usuariorol");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
