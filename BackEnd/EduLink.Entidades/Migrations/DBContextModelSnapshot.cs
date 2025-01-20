﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProbandoMigracionMySQL.Entidades;

#nullable disable

namespace EduLink.Entidades.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Ciudad", b =>
                {
                    b.Property<int>("CiudadId")
                        .HasColumnType("int");

                    b.Property<string>("NombreCiudad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ProvinciaId")
                        .HasColumnType("int");

                    b.HasKey("CiudadId");

                    b.ToTable("Ciudades", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Estudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("CiudadId")
                        .HasColumnType("int");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("EstudianteId");

                    b.HasIndex("CiudadId");

                    b.ToTable("Estudiantes", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.EstudianteExamen", b =>
                {
                    b.Property<int>("EstudianteExamenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EstadoExamen")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<int>("ExamenId")
                        .HasColumnType("int");

                    b.Property<decimal>("Nota")
                        .HasColumnType("decimal(3, 1)");

                    b.HasKey("EstudianteExamenId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("ExamenId");

                    b.ToTable("EstudiantesExamenes", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.EstudianteMateria", b =>
                {
                    b.Property<int>("EstudianteMateriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AnioCursado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("EstadoMateria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.HasKey("EstudianteMateriaId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("EstudiantesMaterias", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Examen", b =>
                {
                    b.Property<int>("ExamenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("FechaExamen")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("HoraComienzo")
                        .HasColumnType("time(6)");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.HasKey("ExamenId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Examenes", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Materia", b =>
                {
                    b.Property<int>("MateriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnioDeLaMateria")
                        .HasColumnType("int");

                    b.Property<int>("Correlativa")
                        .HasColumnType("int");

                    b.Property<int>("Cuatrimestre")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.Property<string>("NombreMateria")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("MateriaId");

                    b.HasIndex("ModuloId");

                    b.ToTable("Materias", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Modulo", b =>
                {
                    b.Property<int>("ModuloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("HoraEntrada")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("HoraSalida")
                        .HasColumnType("time(6)");

                    b.HasKey("ModuloId");

                    b.ToTable("Modulos", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Provincia", b =>
                {
                    b.Property<int>("ProvinciaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreProvincia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ProvinciaId");

                    b.ToTable("Provincias", (string)null);
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Ciudad", b =>
                {
                    b.HasOne("EduLink.Entidades.EntidadesBDD.Provincia", "Provincia")
                        .WithMany("Ciudades")
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provincia");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Estudiante", b =>
                {
                    b.HasOne("EduLink.Entidades.EntidadesBDD.Ciudad", "Ciudad")
                        .WithMany("Estudiantes")
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ciudad");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.EstudianteExamen", b =>
                {
                    b.HasOne("EduLink.Entidades.EntidadesBDD.Estudiante", "Estudiante")
                        .WithMany("EstudiantesExamenes")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLink.Entidades.EntidadesBDD.Examen", "Examen")
                        .WithMany("EstudiantesExamenes")
                        .HasForeignKey("ExamenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Examen");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.EstudianteMateria", b =>
                {
                    b.HasOne("EduLink.Entidades.EntidadesBDD.Estudiante", "Estudiante")
                        .WithMany("EstudiantesMaterias")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLink.Entidades.EntidadesBDD.Materia", "Materia")
                        .WithMany("EstudiantesMaterias")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Examen", b =>
                {
                    b.HasOne("EduLink.Entidades.EntidadesBDD.Materia", "Materia")
                        .WithMany("Examenes")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Materia", b =>
                {
                    b.HasOne("EduLink.Entidades.EntidadesBDD.Modulo", "Modulo")
                        .WithMany("Materias")
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Ciudad", b =>
                {
                    b.Navigation("Estudiantes");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Estudiante", b =>
                {
                    b.Navigation("EstudiantesExamenes");

                    b.Navigation("EstudiantesMaterias");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Examen", b =>
                {
                    b.Navigation("EstudiantesExamenes");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Materia", b =>
                {
                    b.Navigation("EstudiantesMaterias");

                    b.Navigation("Examenes");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Modulo", b =>
                {
                    b.Navigation("Materias");
                });

            modelBuilder.Entity("EduLink.Entidades.EntidadesBDD.Provincia", b =>
                {
                    b.Navigation("Ciudades");
                });
#pragma warning restore 612, 618
        }
    }
}
