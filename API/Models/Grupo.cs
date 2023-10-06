namespace API;

public class Grupo
{
    public Grupo() => CriadoEm = DateTime.Now;

    public int GrupoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime CriadoEm { get; set; }
}
