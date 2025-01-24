using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace EduLink.Entidades.Migrations
{
    /// <inheritdoc />
    public partial class RezaMalenaReza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HoraEntrada = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    HoraSalida = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.ModuloId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    ProvinciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreProvincia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.ProvinciaId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreMateria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AnioDeLaMateria = table.Column<int>(type: "int", nullable: false),
                    Cuatrimestre = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Correlativa = table.Column<int>(type: "int", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.MateriaId);
                    table.ForeignKey(
                        name: "FK_Materias_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "ModuloId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    NombreCiudad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ProvinciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadId);
                    table.ForeignKey(
                        name: "FK_Ciudades_Provincias_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Provincias",
                        principalColumn: "ProvinciaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Examenes",
                columns: table => new
                {
                    ExamenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    HoraComienzo = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    FechaExamen = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.ExamenId);
                    table.ForeignKey(
                        name: "FK_Examenes_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DNI = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Contraseña = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteId);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "CiudadId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstudiantesExamenes",
                columns: table => new
                {
                    EstudianteExamenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    ExamenId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    EstadoExamen = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudiantesExamenes", x => x.EstudianteExamenId);
                    table.ForeignKey(
                        name: "FK_EstudiantesExamenes_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudiantesExamenes_Examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "Examenes",
                        principalColumn: "ExamenId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstudiantesMaterias",
                columns: table => new
                {
                    EstudianteMateriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    AnioCursado = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    EstadoMateria = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudiantesMaterias", x => x.EstudianteMateriaId);
                    table.ForeignKey(
                        name: "FK_EstudiantesMaterias_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudiantesMaterias_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_CiudadId",
                table: "Estudiantes",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesExamenes_EstudianteId",
                table: "EstudiantesExamenes",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesExamenes_ExamenId",
                table: "EstudiantesExamenes",
                column: "ExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesMaterias_EstudianteId",
                table: "EstudiantesMaterias",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesMaterias_MateriaId",
                table: "EstudiantesMaterias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_MateriaId",
                table: "Examenes",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_ModuloId",
                table: "Materias",
                column: "ModuloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstudiantesExamenes");

            migrationBuilder.DropTable(
                name: "EstudiantesMaterias");

            migrationBuilder.DropTable(
                name: "Examenes");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
