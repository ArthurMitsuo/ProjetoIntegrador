namespace API;

public class UsuarioOperacional : Usuario
{
    public Grupo? Grupo { get; set; }
    public int? GrupoId { get; set; }
    public string Tipo { get; set; } = "OPERACIONAL";
}
