﻿// <auto-generated />
using System;
using ATM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ATM.Migrations
{
    [DbContext(typeof(CajeroAutomaticoDBContext))]
    [Migration("20240504204857_AgregarIntentosFallidosATarjeta")]
    partial class AgregarIntentosFallidosATarjeta
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ATM.Operacione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CodigoOperacion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime");

                    b.Property<int>("Idtarjeta")
                        .HasColumnType("int")
                        .HasColumnName("IDTarjeta");

                    b.Property<decimal?>("MontoRetirado")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("Idtarjeta");

                    b.ToTable("Operaciones");
                });

            modelBuilder.Entity("ATM.Tarjeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("Bloqueada")
                        .HasColumnType("bit");

                    b.Property<int>("IntentosFallidos")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)");

                    b.HasKey("Id");

                    b.ToTable("Tarjetas");
                });

            modelBuilder.Entity("ATM.Operacione", b =>
                {
                    b.HasOne("ATM.Tarjeta", "IdtarjetaNavigation")
                        .WithMany("Operaciones")
                        .HasForeignKey("Idtarjeta")
                        .IsRequired()
                        .HasConstraintName("FK__Operacion__IDTar__3B75D760");

                    b.Navigation("IdtarjetaNavigation");
                });

            modelBuilder.Entity("ATM.Tarjeta", b =>
                {
                    b.Navigation("Operaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
