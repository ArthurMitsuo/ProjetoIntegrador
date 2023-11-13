using System.ComponentModel.DataAnnotations.Schema;

namespace API;

public class Usuario
{
    internal string? Tipo= string.Empty;

    public Usuario() => DataCadastro = DateTime.Now;

    public int UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public string? DataNascimento { get; set; }
    //public string? Tipo { get; set; }
    public bool? Logado { get; set; } = false;
    public ICollection<Tarefa> Tarefa { get; set;} = new List<Tarefa>();
    public DateTime DataCadastro { get; set; }

}