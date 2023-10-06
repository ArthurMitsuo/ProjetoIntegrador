namespace API;

public class Tarefa
{
    public Tarefa() => CriadoEm = DateTime.Now;

    public int TarefaId { get; set; }
    public string? Nome { get; set; }
    public Status Status { get; set; }
    public Usuario Usuario { get; set; }
    public string? Descricao { get; set; }
    public string? Corpo { get; set; }
    public Prioridade Prioridade { get; set; }
    public DateTime CriadoEm { get; set; }
}
