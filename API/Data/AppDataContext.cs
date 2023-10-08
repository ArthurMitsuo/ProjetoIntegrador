using API;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDataContext : DbContext
{
     public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }

    //Classes que vão se tornar tabelas no banco de dados
    public DbSet<Cargo> Cargos {get; set;}
    public DbSet<Grupo> Grupos {get; set;}
    public DbSet<Prioridade> Prioridades {get; set;}
    public DbSet<Status> Status {get; set;}
    public DbSet<Tarefa> Tarefas {get; set;}
    public DbSet<TarefaAtividade> TarefaAtividade {get; set;}
    public DbSet<TarefaProjeto> TarefaProjeto {get; set;}
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<UsuarioOperacional> UsuariosOperacionais {get; set;}
    public DbSet<UsuarioGerencial> UsuariosGerenciais {get; set;}
    public DbSet<UsuarioAdmin> UsuariosAdmin {get; set;}

    //Mapeamento da herança das classes Tarefa e Usuario com TUTTH, mapeando e criando novo campo com o valor
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>()
            .HasDiscriminator<string>("Tipo")
            .HasValue<TarefaAtividade>("Atividade")
            .HasValue<TarefaProjeto>("Projeto");

        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("Tipo")
            .HasValue<UsuarioAdmin>("Admin")
            .HasValue<UsuarioGerencial>("Gerencial")
            .HasValue<UsuarioOperacional>("Operacional");    

        modelBuilder.Entity<Grupo>()
            .HasMany(g => g.UsuariosOperacionais) 
            .WithOne(u => u.Grupo)    
            .HasForeignKey(u => u.UsuarioId); 

        base.OnModelCreating(modelBuilder);

    }
}
