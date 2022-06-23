﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MusicProWeb.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<SubCat> SubCats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("DEV");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MUSICPRODB")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("CATEGORIA_PROD_PK");

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.IdCategoria)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.Categorias)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORIAS");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("MARCA_PK");

                entity.ToTable("MARCA");

                entity.Property(e => e.IdMarca)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("ID_MARCA");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Proveedor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROVEEDOR");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => new { e.IdProd, e.SubCatId, e.MarcaId })
                    .HasName("PRODUCTO_PK");

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.IdProd)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("ID_PROD");

                entity.Property(e => e.SubCatId)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("SUB_CAT_ID");

                entity.Property(e => e.MarcaId)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("MARCA_ID");

                entity.Property(e => e.CantidadStock)
                    .HasColumnType("NUMBER(20)")
                    .HasColumnName("CANTIDAD_STOCK");

                entity.Property(e => e.CategoriaId)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("CATEGORIA_ID");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IMAGEN");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("MODELO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Precio)
                    .HasColumnType("NUMBER(20)")
                    .HasColumnName("PRECIO");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PRODUCTO_MARCA_FK");

                entity.HasOne(d => d.SubCat)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => new { d.SubCatId, d.CategoriaId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PRODUCTO_SUB_CAT_FK");
            });

            modelBuilder.Entity<SubCat>(entity =>
            {
                entity.HasKey(e => new { e.IdSub, e.CategoriaId })
                    .HasName("SUB_CAT_PK");

                entity.ToTable("SUB_CAT");

                entity.Property(e => e.IdSub)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("ID_SUB");

                entity.Property(e => e.CategoriaId)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("CATEGORIA_ID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.SubCats)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SUB_CAT_CATEGORIA_FK");
            });

            modelBuilder.HasSequence("SEQ_ERROR");

            modelBuilder.HasSequence("SUCURSAL_SUCURSAL_ID_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
