namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

[ApiController]
[Route("api/tarefa")]
public class TarefaController : ControllerBase
{
    private readonly AppDataContext _context;
    public TarefaController(AppDataContext context){
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Tarefa> tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: para todos os usuários
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Tarefa tarefa)
    {
        try
        {
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            Tarefa? tarefaCadastro = _context.Tarefas.Find(id);
            if (tarefaCadastro != null)
            {
                _context.Tarefas.Remove(tarefaCadastro);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public IActionResult Alterar([FromRoute] int id,
        [FromBody] Tarefa tarefa)
    {
        try
        {
            //Expressões lambda
            Tarefa? tarefaCadastro =
                _context.Tarefas.FirstOrDefault(x => x.TarefaId == id);

            if (tarefaCadastro != null)
            {
                tarefaCadastro.Nome = tarefa.Nome;
                tarefaCadastro.Usuario = tarefa.Usuario;
                tarefaCadastro.Descricao = tarefa.Descricao;
                tarefaCadastro.Corpo = tarefa.Corpo;
                tarefaCadastro.Prioridade = tarefa.Prioridade;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}