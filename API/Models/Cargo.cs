namespace API;

public class Cargo
{
    public Cargo() => CriadoEm = DateTime.Now;

    public int CargoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public ICollection<UsuarioGerencial> UsuariosGerenciais { get; } = new List<UsuarioGerencial>();
    public ICollection<UsuarioOperacional> UsuariosOperacionais { get; } = new List<UsuarioOperacional>();
    public DateTime CriadoEm { get; set; }
}