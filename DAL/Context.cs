
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.DAL
{
    public class Context : DbContext{
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=Data/Prestamos.db");
        }
    }
}