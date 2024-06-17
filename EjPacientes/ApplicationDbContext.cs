using EjPacientes.Entities;
using Microsoft.EntityFrameworkCore;

namespace EjPacientes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Receta> Recetas { get; set; }

    }
}