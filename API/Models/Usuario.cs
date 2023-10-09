using System.ComponentModel.DataAnnotations.Schema;

namespace API;

public class Usuario
{
    public Usuario() => DataCadastro = DateTime.Now;

    public int UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public string? DataNascimento { get; set; }
    public bool? Logado { get; set; }
    public string? Tipo { get; set; }
    [NotMapped]
    public ICollection<Tarefa>? Tarefa { get; set;}
    public DateTime DataCadastro { get; set; }
}