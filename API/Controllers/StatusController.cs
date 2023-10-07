namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

[ApiController]
[Route("api/status")]
public class StatusController : ControllerBase
{
    private readonly AppDataContext _context;
    public StatusController(AppDataContext context){
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Status> status = _context.Status.ToList();
            return Ok(status);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/categoria/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Status status)
    {
        try
        {
            _context.Add(status);
            _context.SaveChanges();
            return Created("", status);
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
            Status? statusCadastro = _context.Status.Find(id);
            if (statusCadastro != null)
            {
                _context.Status.Remove(statusCadastro);
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
        [FromBody] Status status)
    {
        try
        {
            //Expressões lambda
            Status? statusCadastro =
                _context.Status.FirstOrDefault(x => x.StatusId == id);

            if (statusCadastro != null)
            {
                statusCadastro.Nome = status.Nome;
                statusCadastro.Descricao = status.Descricao;
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
