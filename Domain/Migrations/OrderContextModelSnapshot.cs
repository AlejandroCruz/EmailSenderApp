﻿// <auto-generated />
using System;
using OrderTaxProcessor.DataInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OrderTaxProcessor.Domain.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderTaxProcessor.Domain.DataEntities.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(2)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2(2)");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Error")
                        .HasColumnType("bit");

                    b.Property<string>("FreightCode")
                        .IsRequired()
                        .HasColumnType("nchar(1)");

                    b.Property<DateTime>("FreightDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("FreightEndtime")
                        .HasColumnType("time(3)");

                    b.Property<TimeSpan>("FreightStarttime")
                        .HasColumnType("time(3)");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPayProcessed")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRetrieved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<decimal>("OrderAmount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("OrderTax")
                        .HasColumnType("decimal(6,3)");

                    b.Property<string>("PayTransNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PickupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StateCode")
                        .IsRequired()
                        .HasColumnType("nchar(2)");

                    b.Property<string>("TransMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
