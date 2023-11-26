namespace API;

public class Grupo
{
    public Grupo() => CriadoEm = DateTime.Now;
    public int GrupoId { get; set; }
    public string? Nome { get; set; }
    public ICollection<UsuarioOperacional> UsuariosOperacionais { get; set; } = new List<UsuarioOperacional>();
    public UsuarioGerencial? Gerenciador { get; set;} = null;
    public string? Descricao { get; set; }
    public DateTime CriadoEm { get; set; }
}
