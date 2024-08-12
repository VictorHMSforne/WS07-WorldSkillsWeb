using Microsoft.EntityFrameworkCore;
using WS.Models;

namespace WS.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Exame> Exames { get; set; }
        public DbSet<Paciente> Paciente { get; set;}
    }
}
