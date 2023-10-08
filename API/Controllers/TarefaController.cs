namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

[ApiController]
[Route("api/tarefa")]
public class TarefaController : ControllerBase, ManipulacaoTarefa
{
    private readonly AppDataContext _context;
    public TarefaController(AppDataContext context){
        _context = context;
    }
    
    //GET - apenas os usuarios Gerencial e Admin
    [HttpGet]
    [Route("listar")]
    public IActionResult ListarTarefa(){
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

    //POST: para todos os usuários
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult CadastrarTarefa([FromBody] Tarefa tarefa)
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

    //DELETE - apenas para o Admin pode
    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult DeletarTarefa([FromRoute] int id)
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

    //PUT - apenas o Admin pode
    [HttpPut]
    [Route("alterar/{id}")]
    public IActionResult AlterarTarefa([FromRoute] int id,
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
                //Comentários são adicionados por usuários GERENCIAL e ADMIN
                //tarefaCadastro.Comentarios = tarefa.Comentarios;
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

    //Método de adicionar comentário em tarefa
    [HttpPut]
    [Route("adicionarComentario/{id}")]
    public IActionResult AdicionarComentario([FromRoute]int id, 
        [FromBody] Tarefa tarefa)
    {
        try
        {
            // Primeiro, verifique se a tarefa existe
             Tarefa? tarefaCadastro = _context.Tarefas.FirstOrDefault(x => x.TarefaId == id);

            if (tarefaCadastro != null)
            {
                tarefaCadastro.Comentarios = tarefa.Comentarios;
                _context.SaveChanges();
                return Ok();
            }else{
                return NotFound("Tarefa não encontrada");
            }             
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}