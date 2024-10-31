﻿// <auto-generated />
using System;
using ConfigurazioniSvc.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConfigurazioniSvc.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241029120248_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConfigurazioniSvc.DataAccessLayer.Entities.Configurazione", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by_user_id");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time");

                    b.Property<Guid?>("DeletedByUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by_user_id");

                    b.Property<DateTime?>("DeletedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_date_time");

                    b.Property<Guid>("FestaId")
                        .HasColumnType("uuid")
                        .HasColumnName("festa_id");

                    b.Property<Guid?>("ModifiedByUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<bool>("Obbligatorio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("obbligatorio");

                    b.Property<int>("Posizione")
                        .HasColumnType("integer")
                        .HasColumnName("posizione");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Scope");

                    b.Property<string>("Tipo")
                        .HasColumnType("text")
                        .HasColumnName("tipo");

                    b.Property<string>("Valore")
                        .HasColumnType("text")
                        .HasColumnName("valore");

                    b.HasKey("Id")
                        .HasName("pk_configurazione");

                    b.ToTable("Configurazione", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}