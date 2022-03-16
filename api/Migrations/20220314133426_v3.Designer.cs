﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Models;

namespace api.Migrations
{
    [DbContext(typeof(KaficContext))]
    [Migration("20220314133426_v3")]
    partial class v3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api.Models.Kafic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Kafici");
                });

            modelBuilder.Entity("api.Models.Porudzbenica", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("porudzbinaid")
                        .HasColumnType("int");

                    b.Property<int?>("proizvodid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("porudzbinaid");

                    b.HasIndex("proizvodid");

                    b.ToTable("spojPorudzbine");
                });

            modelBuilder.Entity("api.Models.Porudzbina", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("obradjena")
                        .HasColumnType("bit");

                    b.Property<int?>("stolicaid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("stolicaid");

                    b.ToTable("Porudzbine");
                });

            modelBuilder.Entity("api.Models.Proizvod", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("cena")
                        .HasColumnType("float");

                    b.Property<string>("naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Proizvodi");
                });

            modelBuilder.Entity("api.Models.Sto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("kaficid")
                        .HasColumnType("int");

                    b.Property<string>("oznaka")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("kaficid");

                    b.ToTable("Stolovi");
                });

            modelBuilder.Entity("api.Models.Stolica", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("oznaka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("slobodna")
                        .HasColumnType("bit");

                    b.Property<int?>("stoid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("stoid");

                    b.ToTable("Stolice");
                });

            modelBuilder.Entity("api.Models.Porudzbenica", b =>
                {
                    b.HasOne("api.Models.Porudzbina", "porudzbina")
                        .WithMany("proizvodi")
                        .HasForeignKey("porudzbinaid");

                    b.HasOne("api.Models.Proizvod", "proizvod")
                        .WithMany("narudzbine")
                        .HasForeignKey("proizvodid");

                    b.Navigation("porudzbina");

                    b.Navigation("proizvod");
                });

            modelBuilder.Entity("api.Models.Porudzbina", b =>
                {
                    b.HasOne("api.Models.Stolica", "stolica")
                        .WithMany("porudzbine")
                        .HasForeignKey("stolicaid");

                    b.Navigation("stolica");
                });

            modelBuilder.Entity("api.Models.Sto", b =>
                {
                    b.HasOne("api.Models.Kafic", "kafic")
                        .WithMany("stolovi")
                        .HasForeignKey("kaficid");

                    b.Navigation("kafic");
                });

            modelBuilder.Entity("api.Models.Stolica", b =>
                {
                    b.HasOne("api.Models.Sto", "sto")
                        .WithMany("stolice")
                        .HasForeignKey("stoid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("sto");
                });

            modelBuilder.Entity("api.Models.Kafic", b =>
                {
                    b.Navigation("stolovi");
                });

            modelBuilder.Entity("api.Models.Porudzbina", b =>
                {
                    b.Navigation("proizvodi");
                });

            modelBuilder.Entity("api.Models.Proizvod", b =>
                {
                    b.Navigation("narudzbine");
                });

            modelBuilder.Entity("api.Models.Sto", b =>
                {
                    b.Navigation("stolice");
                });

            modelBuilder.Entity("api.Models.Stolica", b =>
                {
                    b.Navigation("porudzbine");
                });
#pragma warning restore 612, 618
        }
    }
}
