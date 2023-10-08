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

    //PUT - adicionar e retirar usuários Operadores
    [HttpPut]
    [Route("adicionarUsuario/{idGrupo}/{idUsuario}")]
    public IActionResult AdicionaUsuarioUsuario([FromRoute] int idGrupo, [FromRoute] int idUsuario)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == idGrupo);
            
            UsuarioOperacional? usuarioOperacional =  
                _context.UsuariosOperacionais.FirstOrDefault(x => x.UsuarioId == idUsuario);

            if(usuarioOperacional != null && usuarioOperacional.Tipo == "Operacional"){
                if (grupoCadastrado != null)   {
                    grupoCadastrado.UsuariosOperacionais.Add(usuarioOperacional);
                    _context.Grupos.Update(grupoCadastrado);
                    _context.SaveChanges();
                    return Ok();
                }else{
                    return NotFound("Grupo não encontrado");
                }
            }else{
                return NotFound("Usuario não encontrado ou não é usupario Operacional");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("deletarUsuario/{idGrupo}/{idUsuario}")]
    public IActionResult DeletarUsuario([FromRoute] int idGrupo, [FromRoute] int idUsuario)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == idGrupo);
            
            Usuario? usuarioOperacional =  
                _context.Usuarios.FirstOrDefault(x => x.UsuarioId == idUsuario);
            
            int index = 0;

            if(usuarioOperacional != null && usuarioOperacional.Tipo == "Operacional"){
                if (grupoCadastrado != null)
                    {   
                        List<UsuarioOperacional>? listaAux = grupoCadastrado.UsuariosOperacionais;
                        if(listaAux == null){
                            return NotFound("Nenhum usuário no grupo ainda, impossível deletar");
                        }
                        for(int i = 0; i < listaAux.Count; i++){
                            if(listaAux[i].UsuarioId==usuarioOperacional.UsuarioId){
                                index = i;
                            }
                        }
                        grupoCadastrado.UsuariosOperacionais.RemoveAt(index);
                        _context.Grupos.Update(grupoCadastrado);
                        _context.SaveChanges();
                        return Ok();
                    }else{
                        return NotFound("Grupo Não encontrado");
                    } 
            }else{
                return NotFound("Usuário não encontrado ou não é usuário operacional");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //PUT - adicionar e retirar usuário Gerenciador
    [HttpPut]
    [Route("adicionarGerencia/{idGrupo}/{idUsuarioGerencial}")]
    public IActionResult AdicionarGerencia([FromRoute] int idGrupo, [FromRoute] int idUsuarioGerencial)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == idGrupo);

            UsuarioGerencial? usuarioGerencial = _context.UsuariosGerenciais.FirstOrDefault(x => x.UsuarioId == idUsuarioGerencial);

            if(usuarioGerencial != null && usuarioGerencial.Tipo == "Gerencial"){
                if (grupoCadastrado != null)
                {
                    grupoCadastrado.Gerenciador = usuarioGerencial;
                    _context.Grupos.Update(grupoCadastrado);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Grupo não existe");
            };

            return NotFound("Usuário não existe ou não é Gerencial");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("deletarGerencia/{idGrupo}")]
    public IActionResult DeletarGerencia([FromRoute] int idGrupo)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == idGrupo);

            if (grupoCadastrado != null)
            {
                if(grupoCadastrado.Gerenciador != null){
                    grupoCadastrado.Gerenciador = null;
                    _context.Grupos.Update(grupoCadastrado);
                    _context.SaveChanges();
                    return Ok();
                }else{
                    return NotFound("Gerenciador não existe nesse grupo");
                }
            }
            return NotFound("Grupo não existe");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
