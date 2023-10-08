namespace API;

public class Grupo
{
    public Grupo() => CriadoEm = DateTime.Now;

    public int GrupoId { get; set; }
    public string? Nome { get; set; }
    public List<UsuarioOperacional>? UsuariosOperacionais { get; set; }
    public UsuarioGerencial? Gerenciador { get; set; }
    public string? Descricao { get; set; }
    public DateTime CriadoEm { get; set; }
}
