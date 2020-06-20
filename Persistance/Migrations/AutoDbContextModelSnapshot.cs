﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance;

namespace Persistance.Migrations
{
    [DbContext(typeof(AutoDbContext))]
    partial class AutoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("Pk_Brand");

                    b.ToTable("tBrand");
                });

            modelBuilder.Entity("Domain.Entities.BrandKey", b =>
                {
                    b.Property<int>("BrandId")
                        .HasColumnName("Fk_BrandId")
                        .HasColumnType("int");

                    b.Property<int>("SourceId")
                        .HasColumnName("Fk_SourceId")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId", "SourceId")
                        .HasName("Pk_BrandSource");

                    b.HasIndex("SourceId");

                    b.ToTable("tBrandSource");
                });

            modelBuilder.Entity("Domain.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdUrl")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandId")
                        .HasColumnName("Fk_BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnName("Fk_ModelId")
                        .HasColumnType("int");

                    b.Property<int>("SourceId")
                        .HasColumnName("Fk_SourceId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("Pk_Car");

                    b.HasIndex("AdUrl")
                        .IsUnique()
                        .HasName("UnIndx_AdUrl")
                        .HasFilter("[AdUrl] IS NOT NULL");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.HasIndex("SourceId");

                    b.ToTable("tCar");
                });

            modelBuilder.Entity("Domain.Entities.CarComfort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AirConditioner")
                        .HasColumnType("bit");

                    b.Property<bool>("ElectricMirrors")
                        .HasColumnType("bit");

                    b.Property<bool>("ElectricWindows")
                        .HasColumnType("bit");

                    b.Property<bool>("Stereo")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tCar");
                });

            modelBuilder.Entity("Domain.Entities.CarMainInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Engine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineDisplacement")
                        .HasColumnType("int");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ManufactureDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Millage")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Transmision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tCar");
                });

            modelBuilder.Entity("Domain.Entities.CarSafety", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ABS")
                        .HasColumnType("bit");

                    b.Property<bool>("Airbag")
                        .HasColumnType("bit");

                    b.Property<bool>("Alarm")
                        .HasColumnType("bit");

                    b.Property<bool>("ESP")
                        .HasColumnType("bit");

                    b.Property<bool>("HalogenHeadlights")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tCar");
                });

            modelBuilder.Entity("Domain.Entities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnName("Fk_BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnName("Sk_ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("Pk_Model");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.ToTable("tModel");
                });

            modelBuilder.Entity("Domain.Entities.ModelKey", b =>
                {
                    b.Property<int>("ModelId")
                        .HasColumnName("Fk_ModelId")
                        .HasColumnType("int");

                    b.Property<int>("SourceId")
                        .HasColumnName("Fk_SourceId")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModelId", "SourceId")
                        .HasName("Pk_ModelKey");

                    b.HasIndex("SourceId");

                    b.ToTable("tModelKey");
                });

            modelBuilder.Entity("Domain.Entities.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("Pk_Source");

                    b.ToTable("tSource");
                });

            modelBuilder.Entity("Domain.Entities.BrandKey", b =>
                {
                    b.HasOne("Domain.Entities.Brand", "Brand")
                        .WithMany("BrandKeys")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("Fk_BrandKey_Brand")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Source", "Source")
                        .WithMany("BrandKeys")
                        .HasForeignKey("SourceId")
                        .HasConstraintName("Fk_BrandKey_Source")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Car", b =>
                {
                    b.HasOne("Domain.Entities.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("Fk_Car_Brand")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Model", "Model")
                        .WithMany("Cars")
                        .HasForeignKey("ModelId")
                        .HasConstraintName("Fk_Car_Model")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Source", "Source")
                        .WithMany("Cars")
                        .HasForeignKey("SourceId")
                        .HasConstraintName("Fk_Car_Source")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.CarComfort", b =>
                {
                    b.HasOne("Domain.Entities.Car", null)
                        .WithOne("Comfort")
                        .HasForeignKey("Domain.Entities.CarComfort", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.CarMainInfo", b =>
                {
                    b.HasOne("Domain.Entities.Car", null)
                        .WithOne("MainInfo")
                        .HasForeignKey("Domain.Entities.CarMainInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.CarSafety", b =>
                {
                    b.HasOne("Domain.Entities.Car", null)
                        .WithOne("Safety")
                        .HasForeignKey("Domain.Entities.CarSafety", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Model", b =>
                {
                    b.HasOne("Domain.Entities.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("Fk_Model_Brand")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Model", "SeriesModel")
                        .WithMany("SubModels")
                        .HasForeignKey("ModelId")
                        .HasConstraintName("Sk_SeriesModel")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ModelKey", b =>
                {
                    b.HasOne("Domain.Entities.Model", "Model")
                        .WithMany("ModelKeys")
                        .HasForeignKey("ModelId")
                        .HasConstraintName("Fk_ModelKey_Model")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Source", "Source")
                        .WithMany("ModelKeys")
                        .HasForeignKey("SourceId")
                        .HasConstraintName("Fk_ModelKey_Source")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
