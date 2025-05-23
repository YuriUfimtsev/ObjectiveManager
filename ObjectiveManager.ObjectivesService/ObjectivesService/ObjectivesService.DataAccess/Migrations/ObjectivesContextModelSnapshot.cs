﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ObjectivesService.DataAccess.Models;

#nullable disable

namespace ObjectivesService.DataAccess.Migrations
{
    [DbContext(typeof(ObjectivesContext))]
    partial class ObjectivesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ObjectivesService.Domain.Entities.ObjectiveEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("FinalDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("StatusObjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StatusObjectId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("ObjectivesService.Domain.Entities.StatusObjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ObjectiveId")
                        .HasColumnType("uuid");

                    b.Property<long>("StatusValueId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StatusValueId");

                    b.ToTable("StatusObjects");
                });

            modelBuilder.Entity("ObjectivesService.Domain.Entities.StatusValueEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StatusValues");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Создана"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Приостановлена"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Не достигнута"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Достигнута"
                        });
                });

            modelBuilder.Entity("ObjectivesService.Domain.Entities.ObjectiveEntity", b =>
                {
                    b.HasOne("ObjectivesService.Domain.Entities.StatusObjectEntity", "StatusObject")
                        .WithMany()
                        .HasForeignKey("StatusObjectId");

                    b.Navigation("StatusObject");
                });

            modelBuilder.Entity("ObjectivesService.Domain.Entities.StatusObjectEntity", b =>
                {
                    b.HasOne("ObjectivesService.Domain.Entities.StatusValueEntity", "StatusValue")
                        .WithMany()
                        .HasForeignKey("StatusValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusValue");
                });
#pragma warning restore 612, 618
        }
    }
}
