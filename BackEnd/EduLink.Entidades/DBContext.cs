using EduLink.Entidades.EntidadesBDD;
using Microsoft.EntityFrameworkCore;
using EduLink.Entidades.Enums;

namespace ProbandoMigracionMySQL.Entidades
{
    public class DBContext : DbContext
    {
        public DBContext() { }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<EstudianteExamen> EstudiantesExamenes { get; set; }
        public DbSet<EstudianteMateria> EstudiantesMaterias { get; set; }
        public DbSet<Examen> Examenes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost; Database=edulink; User=root; Password=SofiaAdmin1234;";
            optionsBuilder.UseMySQL(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("Provincias");
                entity.HasKey(e => e.ProvinciaId);//PrimaryKey
                entity.Property(e => e.NombreProvincia).HasMaxLength(50) // Establecer nvarchar(50)
                      .IsRequired(); // No nulleable
                entity.HasMany(e => e.Ciudades)
                      .WithOne(c => c.Provincia)
                      .HasForeignKey(c => c.CiudadId);//ForeyKey
            });


            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.ToTable("Ciudades");
                entity.HasKey(e => e.CiudadId);
                entity.Property(e => e.NombreCiudad).HasMaxLength(50) // Establecer nvarchar(50)
                      .IsRequired(); // No nulleable
                entity.HasMany(e => e.Estudiantes)
                      .WithOne(c => c.Ciudad)
                      .HasForeignKey(c => c.EstudianteId);
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("Estudiantes");
                entity.HasKey(e => e.EstudianteId);
                entity.Property(e => e.Nombres).HasMaxLength(50) // Establecer nvarchar(100)
                      .IsRequired(); // No nulleable
                entity.Property(e => e.Apellidos).HasMaxLength(50) // Establecer nvarchar(100)
                     .IsRequired(); // No nulleable
                entity.Property(e => e.Direccion).HasMaxLength(50) // Establecer nvarchar(150)
                     .IsRequired(); // No nulleable
                entity.Property(e => e.DNI).HasMaxLength(50) // Establecer nvarchar(50)
                     .IsRequired(); // No nulleable
                entity.Property(e => e.Contraseña).HasMaxLength(50) // Establecer nvarchar(50)
                     .IsRequired(); // No nulleable
                entity.Property(e => e.Telefono).HasMaxLength(50) // Establecer nvarchar(50)
                     .IsRequired(); // No nulleable
                entity.Property(e => e.Email).HasMaxLength(50) // Establecer nvarchar(50)
                     .IsRequired(); // No nulleable
                entity.Property(e => e.CiudadId).IsRequired();

                // Añadir propiedades de navegación
                entity.HasMany(e => e.EstudiantesExamenes)
                .WithOne(ex => ex.Estudiante)
                .HasForeignKey(ex => ex.EstudianteId);

                entity.HasMany(e => e.EstudiantesMaterias)
                .WithOne(em => em.Estudiante)
                .HasForeignKey(em => em.EstudianteId);
            });

            modelBuilder.Entity<EstudianteExamen>(entity =>
            {
                entity.ToTable("EstudiantesExamenes");
                entity.HasKey(e => e.EstudianteExamenId);
                entity.Property(e => e.Nota).HasColumnType("decimal(3, 1)"); // nulo porque creo el registro de constancia,pero luego será la nota
                entity.Property(e => e.EstadoExamen) //ENUM!!!
                      .HasConversion<string>()
                      .HasMaxLength(50)
                      .IsRequired();
                entity.HasOne(e => e.Estudiante)
                .WithMany(e => e.EstudiantesExamenes)
                .HasForeignKey(e => e.EstudianteId);
                entity.HasOne(e => e.Examen)
                .WithMany(e => e.EstudiantesExamenes)
                .HasForeignKey(e => e.ExamenId);

            });

            modelBuilder.Entity<Examen>(entity =>
            {
                entity.ToTable("Examenes");
                entity.HasKey(e => e.ExamenId);
                entity.Property(e => e.HoraComienzo)
                .IsRequired();
                entity.Property(e => e.FechaExamen)
                .IsRequired();
                entity.HasOne(e => e.Materia)
                .WithMany(m => m.Examenes)
                .HasForeignKey(e => e.MateriaId);
                entity.HasMany(e => e.EstudiantesExamenes)
                .WithOne(ex => ex.Examen)
                .HasForeignKey(ex => ex.ExamenId);
            });

            modelBuilder.Entity<EstudianteMateria>(entity =>
            {
                entity.ToTable("EstudiantesMaterias");
                entity.HasKey(e => e.EstudianteMateriaId);
                entity.Property(e => e.AnioCursado)
                .HasMaxLength(2).IsRequired();
                entity.Property(e => e.EstadoMateria) // ENUM!!!
                .HasConversion<string>().HasMaxLength(50)
                .IsRequired();
                entity.HasOne(e => e.Estudiante)
                .WithMany(e => e.EstudiantesMaterias)
                .HasForeignKey(e => e.EstudianteId);
                entity.HasOne(e => e.Materia)
                .WithMany(m => m.EstudiantesMaterias)
                .HasForeignKey(e => e.MateriaId);
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.ToTable("Materias");
                entity.HasKey(e => e.MateriaId);
                entity.Property(e => e.NombreMateria)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
                entity.Property(e => e.AnioDeLaMateria)
                .IsRequired();
                entity.Property(e => e.Cuatrimestre); //nulleable
                entity.Property(e => e.Fecha)
                .IsRequired();
                entity.Property(e => e.Correlativa)// recibe 0 si no tiene
                .IsRequired();

                entity.HasOne(e => e.Modulo)
                .WithMany(m => m.Materias)
                .HasForeignKey(e => e.ModuloId);

                entity.HasMany(e => e.EstudiantesMaterias)
                .WithOne(em => em.Materia)
                .HasForeignKey(em => em.MateriaId);

                entity.HasMany(e => e.Examenes)
                .WithOne(ex => ex.Materia)
                .HasForeignKey(ex => ex.MateriaId);
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("Modulos");
                entity.HasKey(e => e.ModuloId);
                entity.Property(e => e.HoraEntrada)
                .IsRequired();
                entity.Property(e => e.HoraSalida)
                .IsRequired();
                entity.HasMany(e => e.Materias)
                .WithOne(m => m.Modulo)
                .HasForeignKey(m => m.ModuloId);
            });

        }
    }
}
