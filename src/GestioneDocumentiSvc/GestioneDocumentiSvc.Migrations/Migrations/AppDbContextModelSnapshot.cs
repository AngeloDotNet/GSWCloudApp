﻿// <auto-generated />
using System;
using GestioneDocumentiSvc.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestioneDocumentiSvc.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GestioneDocumentiSvc.DataAccessLayer.Entities.Documento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DeletedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FestaId")
                        .HasColumnType("uuid");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ModifiedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NomeDocumento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documenti", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
