﻿namespace API;

public class Prioridade
{
    public Prioridade() => CriadoEm = DateTime.Now;

    public int PrioridadeId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Tarefa> Tarefa { get; set;} = new List<Tarefa>();
    public DateTime CriadoEm { get; set; }
}
