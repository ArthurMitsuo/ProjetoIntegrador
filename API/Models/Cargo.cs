namespace API;

public class Cargo
{
    public Cargo() => CriadoEm = DateTime.Now;

    public int StatusId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime CriadoEm { get; set; }
}
