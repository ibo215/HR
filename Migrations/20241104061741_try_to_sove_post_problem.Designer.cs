﻿// <auto-generated />
using HR.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HR.Migrations
{
    [DbContext(typeof(HRContext))]
    [Migration("20241104061741_try_to_sove_post_problem")]
    partial class try_to_sove_post_problem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            DepartmentName = "HR",
                            IsActive = true
                        },
                        new
                        {
                            DepartmentId = 2,
                            DepartmentName = "IT",
                            IsActive = true
                        },
                        new
                        {
                            DepartmentId = 3,
                            DepartmentName = "Finance",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SalaryTierId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SalaryTierId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DepartmentId = 1,
                            IsActive = true,
                            Name = "Alice Smith",
                            Position = "HR Manager",
                            SalaryTierId = 2
                        },
                        new
                        {
                            EmployeeId = 2,
                            DepartmentId = 2,
                            IsActive = true,
                            Name = "Bob Johnson",
                            Position = "Software Developer",
                            SalaryTierId = 1
                        },
                        new
                        {
                            EmployeeId = 3,
                            DepartmentId = 3,
                            IsActive = true,
                            Name = "Charlie Brown",
                            Position = "Finance Analyst",
                            SalaryTierId = 2
                        },
                        new
                        {
                            EmployeeId = 4,
                            DepartmentId = 2,
                            IsActive = true,
                            Name = "Diana Prince",
                            Position = "Senior Developer",
                            SalaryTierId = 3
                        });
                });

            modelBuilder.Entity("Domain.SalaryTier", b =>
                {
                    b.Property<int>("SalaryTierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryTierId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("SalaryAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalaryTierId");

                    b.ToTable("SalaryTiers");

                    b.HasData(
                        new
                        {
                            SalaryTierId = 1,
                            IsActive = true,
                            SalaryAmount = 3000m,
                            TierName = "Junior"
                        },
                        new
                        {
                            SalaryTierId = 2,
                            IsActive = true,
                            SalaryAmount = 5000m,
                            TierName = "Mid-Level"
                        },
                        new
                        {
                            SalaryTierId = 3,
                            IsActive = true,
                            SalaryAmount = 7000m,
                            TierName = "Senior"
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.HasOne("Domain.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SalaryTier", "SalaryTier")
                        .WithMany("Employees")
                        .HasForeignKey("SalaryTierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("SalaryTier");
                });

            modelBuilder.Entity("Domain.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.SalaryTier", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}