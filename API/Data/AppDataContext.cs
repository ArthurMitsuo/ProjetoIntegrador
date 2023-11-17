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
    public DbSet<TarefaAtividade> TarefasAtividade {get; set;}
    public DbSet<TarefaProjeto> TarefasProjeto {get; set;}
    public DbSet<Usuario> Usuarios {get; set;}
    public DbSet<UsuarioOperacional> UsuariosOperacionais {get; set;}
    public DbSet<UsuarioGerencial> UsuariosGerenciais {get; set;}
    public DbSet<UsuarioAdmin> UsuariosAdmin {get; set;}

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Mapeamento da herança das classes Tarefa e Usuario com TUTTH, mapeando e criando novo campo com o valor.
        modelBuilder.Entity<Tarefa>()
            .HasDiscriminator<string>("Tipo")
            .HasValue<TarefaAtividade>("ATIVIDADE")
            .HasValue<TarefaProjeto>("PROJETO");

        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("Tipo")
            .HasValue<UsuarioAdmin>("ADMIN")
            .HasValue<UsuarioGerencial>("GERENCIAL")
            .HasValue<UsuarioOperacional>("OPERACIONAL");   

        modelBuilder.Entity<Grupo>()
            .HasMany(e => e.UsuariosOperacionais)
            .WithOne(e => e.Grupo)
            .HasForeignKey(e => e.GrupoId)
            .IsRequired(false);

        modelBuilder.Entity<Cargo>()
            .HasMany(e => e.UsuariosGerenciais)
            .WithOne(e => e.Cargo)
            .HasForeignKey(e => e.CargoId)
            .IsRequired(false);


        modelBuilder.Entity<Cargo>()
            .HasMany(e => e.UsuariosOperacionais)
            .WithOne(e => e.Cargo)
            .HasForeignKey(e => e.CargoId)
            .IsRequired(false);

        base.OnModelCreating(modelBuilder);

    }
}
