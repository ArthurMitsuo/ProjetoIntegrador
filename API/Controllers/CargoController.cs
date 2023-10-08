namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

[ApiController]
[Route("api/cargo")]
public class CargoController : ControllerBase
{
    private readonly AppDataContext _context;

    public CargoController(AppDataContext context){
        _context=context;
    }

    // GET: api/cargo/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Cargo> cargos = _context.Cargos.ToList();
            return Ok(cargos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: exclusivo para usuário admin
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Cargo cargo)
    {
        try
        {
            _context.Add(cargo);
            _context.SaveChanges();
            return Created("", cargo);
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
            Cargo? cargoCadastrado = _context.Cargos.Find(id);
            if (cargoCadastrado != null)
            {
                _context.Cargos.Remove(cargoCadastrado);
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
        [FromBody] Cargo cargo)
    {
        try
        {
            //Expressões lambda
            Cargo? cargoCadastrado =
                _context.Cargos.FirstOrDefault(x => x.CargoId == id);

            if (cargoCadastrado != null)
            {
                cargoCadastrado.Nome = cargo.Nome;
                cargoCadastrado.Descricao = cargo.Descricao;
                _context.Cargos.Update(cargoCadastrado);
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
