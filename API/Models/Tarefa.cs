using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API;

public class Tarefa
{
    public Tarefa() => CriadoEm = DateTime.Now;
    public int TarefaId { get; set; }
    public string? Nome { get; set; }
    public Status? Status { get; set; }
    public Usuario? Usuario { get; set; }
    //public int? FUsuarioId { get; set; }
    public string? Descricao { get; set; }
    [NotMapped]
    public List<string>? Comentarios { get; set; }
    public string? Corpo { get; set; }
    public string? Tipo { get; set; }
    public Prioridade? Prioridade { get; set; }
    public DateTime CriadoEm { get; set;}
}

