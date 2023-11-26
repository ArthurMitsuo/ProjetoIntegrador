namespace API;

public class UsuarioGerencial : Usuario
{
    public Grupo? Grupo { get; set; }
    public int? GrupoId { get; set; }
    public Cargo? Cargo { get; set; }
    public int? CargoId { get; set; }

    //public new string Tipo { get; set; } = "GERENCIAL";
}
