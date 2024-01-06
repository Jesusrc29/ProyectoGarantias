﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProyectGarantia.Data;

#nullable disable

namespace ProyectGarantia.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231031235900_migracion5")]
    partial class migracion5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProyectGarantia.Data.ApplicationDbContext+Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("AgenciaId")
                        .HasMaxLength(10)
                        .HasColumnType("integer");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasDefaultValue("0");

                    b.HasKey("Id");

                    b.HasIndex("AgenciaId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ProyectGarantia.Models.Agencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Codigo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Agencias");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Almacen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Codigo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Almacenes");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProyectGarantia.Models.ContadorLotes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CodAgencia")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CodAgencia");

                    b.Property<int>("Contador")
                        .HasColumnType("integer")
                        .HasColumnName("Contador");

                    b.HasKey("Id");

                    b.ToTable("ContadorLotes");
                });

            modelBuilder.Entity("ProyectGarantia.Models.DetalleLote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgenciaId")
                        .HasColumnType("integer")
                        .HasColumnName("AgenciaId");

                    b.Property<int>("CantidadGarantias")
                        .HasColumnType("integer")
                        .HasColumnName("CantidadGarantias");

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer")
                        .HasColumnName("ClienteId");

                    b.Property<DateOnly>("FechaEnvio")
                        .HasColumnType("date")
                        .HasColumnName("FechaEnvio");

                    b.Property<DateOnly>("FechaOtorgada")
                        .HasColumnType("date")
                        .HasColumnName("FechaOtorgada");

                    b.Property<int>("LoteId")
                        .HasColumnType("integer")
                        .HasColumnName("LoteId");

                    b.Property<string>("NombreAsesor")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NombreAsesor");

                    b.HasKey("Id");

                    b.HasIndex("AgenciaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("LoteId");

                    b.ToTable("DetalleLote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.DetalleLoteModelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadGarantias")
                        .HasColumnType("integer")
                        .HasColumnName("CantidadGarantias");

                    b.Property<string>("CentroCosto")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CentroCosto");

                    b.Property<bool>("Contrato")
                        .HasColumnType("boolean")
                        .HasColumnName("Contrato");

                    b.Property<DateOnly>("FechaEnvio")
                        .HasColumnType("date")
                        .HasColumnName("FechaEnvio");

                    b.Property<DateOnly>("FechaOtorgada")
                        .HasColumnType("date")
                        .HasColumnName("FechaOtorgada");

                    b.Property<bool>("GHAvaluo")
                        .HasColumnType("boolean")
                        .HasColumnName("GHAvaluo");

                    b.Property<bool>("GHEscritura")
                        .HasColumnType("boolean")
                        .HasColumnName("GHEscritura");

                    b.Property<bool>("GHRevisionIP")
                        .HasColumnType("boolean")
                        .HasColumnName("GHRevisionIP");

                    b.Property<bool>("GVCopiaRevision")
                        .HasColumnType("boolean")
                        .HasColumnName("GVCopiaRevision");

                    b.Property<bool>("GVDocOriginal")
                        .HasColumnType("boolean")
                        .HasColumnName("GVDocOriginal");

                    b.Property<bool>("GVTraspaso")
                        .HasColumnType("boolean")
                        .HasColumnName("GVTraspaso");

                    b.Property<int>("LoteId")
                        .HasColumnType("integer");

                    b.Property<string>("NombreAsesor")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NombreAsesor");

                    b.Property<string>("NombreCliente")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NombreCliente");

                    b.Property<string>("NombreCreador")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NombreCreador");

                    b.Property<string>("NumContrato")
                        .HasColumnType("text")
                        .HasColumnName("NumContrato");

                    b.Property<string>("NumPagare")
                        .HasColumnType("text")
                        .HasColumnName("NumPagare");

                    b.Property<string>("NumPrestamo")
                        .HasColumnType("text")
                        .HasColumnName("NumPrestamo");

                    b.Property<string>("NumeroCorrelativo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NumeroCorrelativo");

                    b.Property<bool>("Pagare")
                        .HasColumnType("boolean")
                        .HasColumnName("Pagare");

                    b.Property<decimal>("ValorGarantia")
                        .HasColumnType("numeric")
                        .HasColumnName("ValorGarantia");

                    b.HasKey("Id");

                    b.HasIndex("LoteId");

                    b.ToTable("DetalleLoteModelo");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Documentacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DetalleLoteId")
                        .HasColumnType("integer")
                        .HasColumnName("DetalleLoteId");

                    b.Property<int>("TipoOperacion")
                        .HasColumnType("integer")
                        .HasColumnName("TipoOperacion");

                    b.HasKey("Id");

                    b.HasIndex("DetalleLoteId");

                    b.ToTable("Documentaciones");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Garantia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AlmacenId")
                        .HasColumnType("integer")
                        .HasColumnName("AlmacenId");

                    b.Property<int>("DetalleLoteId")
                        .HasColumnType("integer")
                        .HasColumnName("DetalleLoteId");

                    b.Property<int>("Estado")
                        .HasColumnType("integer")
                        .HasColumnName("Estado");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer")
                        .HasColumnName("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("AlmacenId");

                    b.HasIndex("DetalleLoteId");

                    b.ToTable("Garantias");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Lote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Estado")
                        .HasColumnType("integer")
                        .HasColumnName("Estado");

                    b.Property<DateOnly>("FechaCreacion")
                        .HasColumnType("date")
                        .HasColumnName("FechaCreacion");

                    b.Property<DateTime>("FechaDesde")
                        .HasColumnType("Date")
                        .HasColumnName("FechaDesde");

                    b.Property<DateTime>("FechaHasta")
                        .HasColumnType("Date")
                        .HasColumnName("FechaHasta");

                    b.Property<string>("NombreCreador")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NombreCreador");

                    b.Property<string>("NumeroCorrelativo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NumeroCorrelativo");

                    b.HasKey("Id");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProyectGarantia.Data.ApplicationDbContext+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProyectGarantia.Data.ApplicationDbContext+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectGarantia.Data.ApplicationDbContext+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProyectGarantia.Data.ApplicationDbContext+Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectGarantia.Data.ApplicationDbContext+Usuario", b =>
                {
                    b.HasOne("ProyectGarantia.Models.Agencia", "Agencia")
                        .WithMany()
                        .HasForeignKey("AgenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agencia");
                });

            modelBuilder.Entity("ProyectGarantia.Models.DetalleLote", b =>
                {
                    b.HasOne("ProyectGarantia.Models.Agencia", "Agencia")
                        .WithMany("DetallesLote")
                        .HasForeignKey("AgenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectGarantia.Models.Cliente", "Cliente")
                        .WithMany("DetallesLote")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectGarantia.Models.Lote", "Lote")
                        .WithMany("DetallesLote")
                        .HasForeignKey("LoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agencia");

                    b.Navigation("Cliente");

                    b.Navigation("Lote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.DetalleLoteModelo", b =>
                {
                    b.HasOne("ProyectGarantia.Models.Lote", "Lote")
                        .WithMany()
                        .HasForeignKey("LoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Documentacion", b =>
                {
                    b.HasOne("ProyectGarantia.Models.DetalleLote", "DetalleLote")
                        .WithMany("Documentos")
                        .HasForeignKey("DetalleLoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetalleLote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Garantia", b =>
                {
                    b.HasOne("ProyectGarantia.Models.Almacen", "Almacen")
                        .WithMany("Garantias")
                        .HasForeignKey("AlmacenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectGarantia.Models.DetalleLote", "DetalleLote")
                        .WithMany("Garantias")
                        .HasForeignKey("DetalleLoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Almacen");

                    b.Navigation("DetalleLote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Agencia", b =>
                {
                    b.Navigation("DetallesLote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Almacen", b =>
                {
                    b.Navigation("Garantias");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Cliente", b =>
                {
                    b.Navigation("DetallesLote");
                });

            modelBuilder.Entity("ProyectGarantia.Models.DetalleLote", b =>
                {
                    b.Navigation("Documentos");

                    b.Navigation("Garantias");
                });

            modelBuilder.Entity("ProyectGarantia.Models.Lote", b =>
                {
                    b.Navigation("DetallesLote");
                });
#pragma warning restore 612, 618
        }
    }
}
