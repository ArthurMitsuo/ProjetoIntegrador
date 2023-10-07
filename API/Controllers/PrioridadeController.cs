namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

[ApiController]
[Route("api/prioridade")]
public class PrioridadeController : ControllerBase
{
    
    private readonly AppDataContext _context;
    public PrioridadeController(AppDataContext context){
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Prioridade> prioridades = _context.Prioridades.ToList();
            return Ok(prioridades);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/categoria/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Prioridade prioridade)
    {
        try
        {
            _context.Add(prioridade);
            _context.SaveChanges();
            return Created("", prioridade);
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
            Prioridade? prioridadeCadastro = _context.Prioridades.Find(id);
            if (prioridadeCadastro != null)
            {
                _context.Prioridades.Remove(prioridadeCadastro);
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
        [FromBody] Prioridade prioridade)
    {
        try
        {
            //Expressões lambda
            Prioridade? prioridadeCadastro =
                _context.Prioridades.FirstOrDefault(x => x.PrioridadeId == id);

            if (prioridadeCadastro != null)
            {
                prioridadeCadastro.Nome = prioridade.Nome;
                prioridadeCadastro.Descricao = prioridade.Descricao;
                _context.Prioridades.Update(prioridadeCadastro);
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
