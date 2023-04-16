using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TrabajadoresPrueba.Models.DTOs;

namespace TrabajadoresPrueba.Models
{
    public partial class TrabajadoresPruebaContext : DbContext
    {
        public TrabajadoresPruebaContext()
        {
        }

        public TrabajadoresPruebaContext(DbContextOptions<TrabajadoresPruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Distrito> Distritos { get; set; } = null!;
        public virtual DbSet<Provincium> Provincia { get; set; } = null!;
        public virtual DbSet<Trabajadore> Trabajadores { get; set; } = null!;
        public virtual DbSet<TrabajadorDTO> TrabajadoresDTO { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.ToTable("Distrito");

                entity.Property(e => e.NombreDistrito)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Distritos)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK__Distrito__IdProv__2A4B4B5E");
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.Property(e => e.NombreProvincia)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Provincia__IdDep__2B3F6F97");
            });

            modelBuilder.Entity<Trabajadore>(entity =>
            {
                entity.Property(e => e.Nombres)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Trabajado__IdDep__2C3393D0");

                entity.HasOne(d => d.IdDistritoNavigation)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdDistrito)
                    .HasConstraintName("FK__Trabajado__IdDis__2D27B809");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK__Trabajado__IdPro__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
