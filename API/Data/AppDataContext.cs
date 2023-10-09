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
            .HasValue<TarefaAtividade>("Atividade")
            .HasValue<TarefaProjeto>("Projeto");

        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("Tipo")
            .HasValue<UsuarioAdmin>("Admin")
            .HasValue<UsuarioGerencial>("Gerencial")
            .HasValue<UsuarioOperacional>("Operacional");   

        /*modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Tarefa)
            .WithOne(t => t.Usuario)
            .HasForeignKey(e => e.FUsuarioId)
            .IsRequired(false);*/

        modelBuilder.Entity<UsuarioGerencial>()
            .HasOne(g => g.Grupo) 
            .WithOne(u => u.Gerenciador)
            .HasForeignKey<Grupo>(u => u.GrupoId);

        modelBuilder.Entity<Grupo>()
            .HasMany(g => g.UsuariosOperacionais) 
            .WithOne(u => u.Grupo)    
            .HasForeignKey(u => u.GrupoId)
            .IsRequired(false); 

        /*modelBuilder.Entity<Tarefa>()
            .HasNoKey(); */  


        base.OnModelCreating(modelBuilder);

    }
}
