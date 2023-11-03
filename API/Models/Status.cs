namespace API;

public class Status
{
    public Status() => CriadoEm = DateTime.Now;

    public int StatusId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Tarefa>? Tarefa { get; set;}
    public DateTime CriadoEm { get; set; }
}
