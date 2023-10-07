using API;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDataContext : DbContext
{
     public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }

    //Classes que vão se tornar tabelas no banco de dados
    //public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cargo> Cargos {get; set;}
    public DbSet<Grupo> Grupos {get; set;}
    public DbSet<Prioridade> Prioridades {get; set;}
    public DbSet<Status> Status {get; set;}
}
