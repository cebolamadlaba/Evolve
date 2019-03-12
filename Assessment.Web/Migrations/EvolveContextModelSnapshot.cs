﻿// <auto-generated />
using System;
using Assessment.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assessment.Web.Migrations
{
    [DbContext(typeof(EvolveContext))]
    partial class EvolveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assessment.Web.Data.CalculationHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CompletedTime");

                    b.Property<string>("FileName");

                    b.Property<int>("Size");

                    b.Property<string>("Status");

                    b.Property<DateTime>("UploadTime");

                    b.HasKey("Id");

                    b.ToTable("CalculationHeaders");
                });

            modelBuilder.Entity("Assessment.Web.Data.CalculationResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalculationHeaderId");

                    b.Property<string>("Formular");

                    b.Property<double>("InputA");

                    b.Property<double>("InputB");

                    b.Property<double>("InputC");

                    b.Property<double>("Result");

                    b.HasKey("Id");

                    b.HasIndex("CalculationHeaderId");

                    b.ToTable("CalculationResults");
                });

            modelBuilder.Entity("Assessment.Web.Data.CalculationResult", b =>
                {
                    b.HasOne("Assessment.Web.Data.CalculationHeader", "CalculationHeader")
                        .WithMany()
                        .HasForeignKey("CalculationHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
