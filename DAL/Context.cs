
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.DAL
{
    public class Context : DbContext{
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Moras> Moras { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=Data/Prestamos.db");
        }
    }
}