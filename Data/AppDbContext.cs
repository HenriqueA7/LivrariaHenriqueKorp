using LivrariaHenriqueKorp.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaHenriqueKorp.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<LivroModel> Livros { get; set; }
    }
}
