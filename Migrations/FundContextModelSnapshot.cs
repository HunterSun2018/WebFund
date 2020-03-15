﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebFund.Data;

namespace WebFund.Migrations
{
    [DbContext(typeof(FundContext))]
    partial class FundContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("WebFund.Fund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ChangeInDay")
                        .HasColumnType("REAL");

                    b.Property<double>("ChangeInMonth")
                        .HasColumnType("REAL");

                    b.Property<double>("ChangeInWeek")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<double>("value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Funds");
                });
#pragma warning restore 612, 618
        }
    }
}