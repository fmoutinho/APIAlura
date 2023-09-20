using System.Collections.Generic;
using APIAlura.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAlura.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
        {
        }

        public DbSet<Filme> Filmes { get; set; }
    }
}
