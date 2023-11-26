namespace API;

public class UsuarioOperacional : Usuario
{
    public Grupo? Grupo { get; set; }
    public int? GrupoId { get; set; }
    public Cargo? Cargo { get; set; }
    public int? CargoId { get; set; }
    //public new string Tipo { get; set; } = "OPERACIONAL";
}
