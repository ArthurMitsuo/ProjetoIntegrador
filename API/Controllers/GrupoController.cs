namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

[ApiController]
[Route("api/grupo")]
public class GrupoController : ControllerBase{
private readonly AppDataContext _context;

    public GrupoController(AppDataContext context){
        _context=context;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Grupo> grupos = _context.Grupos.ToList();
            return Ok(grupos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: exclusivo para usuário admin
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Grupo grupo)
    {
        try
        {
            _context.Add(grupo);
            _context.SaveChanges();
            return Created("", grupo);
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
            Grupo? grupoCadastrado = _context.Grupos.Find(id);
            if (grupoCadastrado != null)
            {
                _context.Grupos.Remove(grupoCadastrado);
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
        [FromBody] Grupo grupo)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == id);

            if (grupoCadastrado != null)
            {
                grupoCadastrado.Nome = grupo.Nome;
                grupoCadastrado.Descricao = grupo.Descricao;
                _context.Grupos.Update(grupoCadastrado);
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
