namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tarefa")]
public class TarefaController : ControllerBase, ManipulacaoTarefa
{
    private readonly AppDataContext _context;
    public TarefaController(AppDataContext context){
        _context = context;
    }
    
    //GET - apenas os usuarios Gerencial e Admin
    //Projeto
    [HttpGet]
    [Route("projeto/listar")]
    public IActionResult ListarTarefaProjeto(){
        try
        {
            var tarefas = _context.Tarefas
                .Include(u => u.Usuario)
                .Include(p => p.Prioridade)
                .Include(s => s.Status)
                .ToList();

            //List<TarefaProjeto> tarefa = _context.TarefasProjeto.ToList();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //Atividade
    [HttpGet]
    [Route("atividade/listar")]
    public IActionResult ListarTarefaAtividade(){
        try
        {
            var tarefas = _context.Tarefas
                .Include(u => u.Usuario)
                .Include(p => p.Prioridade)
                .Include(s => s.Status)
                .ToList();

            //List<TarefaAtividade> tarefa = _context.TarefasAtividade.ToList();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //POST: para todos os usuários
    //Projeto
    [HttpPost]
    [Route("projeto/cadastrar")]
    public IActionResult CadastrarTarefaProjeto([FromBody] TarefaProjeto tarefa)
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
    //Atividade
    [HttpPost]
    [Route("atividade/cadastrar")]
    public IActionResult CadastrarTarefaAtividade([FromBody] TarefaAtividade tarefa)
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

    //DELETE - apenas o Admin pode
    //Projeto
    [HttpDelete]
    [Route("projeto/deletar/{id}")]
    public IActionResult DeletarTarefaProjeto([FromRoute] int id)
    {
        try
        {
            TarefaProjeto? tarefaCadastro = _context.TarefasProjeto.Find(id);
            if (tarefaCadastro != null)
            {
                _context.TarefasProjeto.Remove(tarefaCadastro);
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
    //Atividade
    [HttpDelete]
    [Route("atividade/deletar/{id}")]
    public IActionResult DeletarTarefaAtividade([FromRoute] int id)
    {
        try
        {
            TarefaAtividade? tarefaCadastro = _context.TarefasAtividade.Find(id);
            if (tarefaCadastro != null)
            {
                _context.TarefasAtividade.Remove(tarefaCadastro);
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
    //Projeto
    [HttpPut]
    [Route("projeto/alterar/{id}")]
    public IActionResult AlterarTarefaProjeto([FromRoute] int id,
        [FromBody] TarefaProjeto tarefa)
    {
        try
        {
            //Expressões lambda
            TarefaProjeto? tarefaCadastro =
                _context.TarefasProjeto.FirstOrDefault(x => x.TarefaId == id);

            if (tarefaCadastro != null)
            {
                tarefaCadastro.Nome = tarefa.Nome;
                tarefaCadastro.Usuario = tarefa.Usuario;
                tarefaCadastro.Descricao = tarefa.Descricao;
                //Comentários são adicionados por usuários GERENCIAL e ADMIN
                tarefaCadastro.Corpo = tarefa.Corpo;
                tarefaCadastro.Prioridade = tarefa.Prioridade;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound("Tarefa não encontrada");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //Atividade
    [HttpPut]
    [Route("atividade/alterar/{id}")]
    public IActionResult AlterarTarefaAtividade([FromRoute] int id,
        [FromBody] TarefaAtividade tarefa)
    {
        try
        {
            //Expressões lambda
            TarefaAtividade? tarefaCadastro =
                _context.TarefasAtividade.FirstOrDefault(x => x.TarefaId == id);

            if (tarefaCadastro != null)
            {
                tarefaCadastro.Nome = tarefa.Nome;
                tarefaCadastro.Usuario = tarefa.Usuario;
                tarefaCadastro.Descricao = tarefa.Descricao;
                //Comentários são adicionados por usuários GERENCIAL e ADMIN
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

    //Método de adicionar comentário em tarefa - apenas Users Admin e Gerencial
    [HttpPut]
    [Route("adicionarComentario/{id}")]
    public IActionResult AdicionarComentario([FromRoute]int id, 
        [FromBody] string comentario)
    {
        try
        {
            // Primeiro, verifique se a tarefa existe
            Tarefa? tarefaCadastro = _context.Tarefas.FirstOrDefault(x => x.TarefaId == id);


            if (tarefaCadastro != null)
            {
                tarefaCadastro.Comentarios = new List<String>();
                tarefaCadastro.Comentarios.Add(comentario);
                _context.Tarefas.Update(tarefaCadastro);
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

    //Método de alterar Status em tarefa - apenas Users Admin e Gerencial
    [HttpPut]
    [Route("alterar-status/{idTarefa}/{idStatus}")]
    public async Task<IActionResult> AlteraStatus([FromRoute]int idTarefa, [FromRoute]int idStatus)
    {
        try
        {
            // Primeiro, verifique se a tarefa existe
            Tarefa? tarefaCadastro = await _context.Tarefas
                .Include(s => s.Status)
                .FirstOrDefaultAsync(x => x.TarefaId == idTarefa);

            Status? status = await _context.Status.FirstOrDefaultAsync(x=>x.StatusId == idStatus);

            if(status == null){
                return NotFound("Status não encontrado");
            }

            if (tarefaCadastro != null)
            {
                tarefaCadastro.Status = status;
                _context.Tarefas.Update(tarefaCadastro);
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

    //Método de alterar Prioridade em tarefa - apenas Users Admin e Gerencial
    [HttpPut]
    [Route("alterar-status/{idTarefa}/{idStatus}")]
    public async Task<IActionResult> AlteraPrioridade([FromRoute]int idTarefa, [FromRoute]int idPrioridade)
    {
        try
        {
            // Primeiro, verifique se a tarefa existe
            Tarefa? tarefaCadastro = await _context.Tarefas
                .Include(s => s.Status)
                .FirstOrDefaultAsync(x => x.TarefaId == idTarefa);

            Prioridade? prioridade = await _context.Prioridades.FirstOrDefaultAsync(x=>x.PrioridadeId == idPrioridade);

            if(prioridade == null){
                return NotFound("Prioridade não encontrado");
            }

            if (tarefaCadastro != null)
            {
                tarefaCadastro.Prioridade = prioridade;
                _context.Tarefas.Update(tarefaCadastro);
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