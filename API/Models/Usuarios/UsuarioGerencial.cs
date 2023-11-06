namespace API;

public class UsuarioGerencial : Usuario
{
    public Grupo? Grupo { get; set; }

    public string Tipo { get; set; } = "GERENCIAL";
}
