namespace API;

public class Usuario
{
    public Usuario() => DataCadastro = DateTime.Now;

    public int UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public string? DataNascimento { get; set; }
    public List<Tarefa>? Tarefas { get; set; }
    public DateTime DataCadastro { get; set; }
}