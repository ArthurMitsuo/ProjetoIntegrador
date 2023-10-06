namespace API;

public class Prioridade
{
    public Prioridade() => CriadoEm = DateTime.Now;

    public int PrioridadeId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime CriadoEm { get; set; }
}
