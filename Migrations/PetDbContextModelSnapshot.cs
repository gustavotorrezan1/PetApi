﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetApi.Data;

#nullable disable

namespace PetApi.Migrations
{
    [DbContext(typeof(PetDbContext))]
    partial class PetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetApi.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<int>("Ativo")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("PetApi.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdutoId"));

                    b.Property<int>("Ativo")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("CodBarras")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrecoCusto")
                        .HasColumnType("float");

                    b.Property<double>("PrecoVenda")
                        .HasColumnType("float");

                    b.Property<int>("SubCategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("UnidadeMedidaId")
                        .HasColumnType("int");

                    b.HasKey("ProdutoId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("SubCategoriaId");

                    b.HasIndex("UnidadeMedidaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("PetApi.Models.SubCategoria", b =>
                {
                    b.Property<int>("SubCategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoriaId"));

                    b.Property<int>("Ativo")
                        .HasColumnType("int");

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("Nome")
                        .HasColumnType("int");

                    b.HasKey("SubCategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("SubCategorias");
                });

            modelBuilder.Entity("PetApi.Models.UnidadeMedida", b =>
                {
                    b.Property<int>("UnidadeMedidaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnidadeMedidaId"));

                    b.Property<int>("Ativo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnidadeMedidaId");

                    b.ToTable("UnidadeMedidas");
                });

            modelBuilder.Entity("PetApi.Models.Produto", b =>
                {
                    b.HasOne("PetApi.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetApi.Models.SubCategoria", "SubCategoria")
                        .WithMany()
                        .HasForeignKey("SubCategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetApi.Models.UnidadeMedida", "UnidadeMedida")
                        .WithMany()
                        .HasForeignKey("UnidadeMedidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("SubCategoria");

                    b.Navigation("UnidadeMedida");
                });

            modelBuilder.Entity("PetApi.Models.SubCategoria", b =>
                {
                    b.HasOne("PetApi.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.Navigation("Categoria");
                });
#pragma warning restore 612, 618
        }
    }
}
