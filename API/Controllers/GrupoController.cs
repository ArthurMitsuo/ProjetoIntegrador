namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/grupo")]
public class GrupoController : ControllerBase{
private readonly AppDataContext _context;

    public GrupoController(AppDataContext context){
        _context=context;
    }

    //GET - listar os grupos
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<UsuarioOperacional> usuarioOperacionals =
                _context.UsuariosOperacionais
                .Include(x => x.Grupo)
                .ToList();

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

        // Agora que o Grupo foi salvo, você pode adicionar UsuariosOperacionais
        // Certifique-se de que a propriedade UsuariosOperacionais esteja inicializada
        if (grupo.UsuariosOperacionais != null)
        {
            foreach (var usuarioOperacional in grupo.UsuariosOperacionais)
            {
                // Adicione a lógica necessária para configurar o relacionamento e adicionar ao contexto
                // Exemplo: usuarioOperacional.GrupoId = grupo.GrupoId;
                _context.Add(usuarioOperacional);
            }

            _context.SaveChanges();
        }

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
    [Route("adicionar-usuario-operacional/{idGrupo}/{idUsuario}")]
    public async Task<IActionResult> AdicionaUsuarioOperacionalAsync([FromRoute] int idGrupo, [FromRoute] int idUsuario)
    {
        try
        {
        // Busca o Grupo
        Grupo? grupoCadastrado = await _context.Grupos
            .Include(g => g.UsuariosOperacionais)  // Certifique-se de incluir a coleção de UsuariosOperacionais
            .FirstOrDefaultAsync(g => g.GrupoId == idGrupo);

        // Busca o UsuarioOperacional
        UsuarioOperacional? usuarioOperacional = await _context.UsuariosOperacionais
            .FirstOrDefaultAsync(uo => uo.UsuarioId == idUsuario);


        // Verifica se ambos foram encontrados
        if (grupoCadastrado != null && usuarioOperacional != null)
        {
            foreach(Usuario usuarios in grupoCadastrado.UsuariosOperacionais){
                if(usuarioOperacional.UsuarioId == usuarios.UsuarioId){
                    return NotFound("Usuário já cadastrado no grupo");
                }
            }
            // Verifica se o usuário é do tipo "OPERACIONAL"
            if (usuarioOperacional.Tipo == "OPERACIONAL")
            {
                // Inicializa a coleção se for nula
                grupoCadastrado.UsuariosOperacionais ??= new List<UsuarioOperacional>();

                // Adiciona o UsuarioOperacional à coleção do Grupo
                grupoCadastrado.UsuariosOperacionais.Add(usuarioOperacional);

                // Atualiza o Grupo no contexto
                _context.Grupos.Update(grupoCadastrado);

                // Salva as alterações
                await _context.SaveChangesAsync();

                // Retorna o objeto modificado, se necessário
                return Ok(grupoCadastrado);
            }
            else
            {
                return NotFound("Usuário não é do tipo OPERACIONAL");
            }
        }
        else
        {
            return NotFound("Grupo ou usuário não encontrado");
        }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut]
    [Route("deletar-usuario-operacional/{idGrupo}/{idUsuario}")]
    public IActionResult DeletarUsuarioOperacional([FromRoute] int idGrupo, [FromRoute] int idUsuario)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == idGrupo);
            
            Usuario? usuarioOperacional =  
                _context.UsuariosOperacionais
                .FirstOrDefault(x => x.UsuarioId == idUsuario);


        
            if(usuarioOperacional != null && usuarioOperacional.Tipo == "OPERACIONAL"){
                if (grupoCadastrado != null)
                    {   
                        ICollection<UsuarioOperacional>? listaAux = grupoCadastrado.UsuariosOperacionais;
                        if(listaAux == null){
                            return NotFound("Nenhum usuário no grupo ainda, impossível deletar");
                        }
                        for(int i = 0; i < listaAux?.Count; i++){
                            if(listaAux.ElementAt(i).UsuarioId==usuarioOperacional?.UsuarioId){
                            _ = (grupoCadastrado.UsuariosOperacionais?.Remove(listaAux.ElementAt(i)));
                            }
                        }
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
    [Route("adicionar-usuario-gerencial/{idGrupo}/{idUsuarioGerencial}")]
    public IActionResult AdicionarUsuarioGerencia([FromRoute] int idGrupo, [FromRoute] int idUsuarioGerencial)
    {
        try
        {
            //Expressões lambda
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == idGrupo);

            UsuarioGerencial? usuarioGerencial = _context.UsuariosGerenciais.FirstOrDefault(x => x.UsuarioId == idUsuarioGerencial);

            if(usuarioGerencial == null){
                if (grupoCadastrado != null)
                {
                    grupoCadastrado.Gerenciador = usuarioGerencial;
                    _context.Grupos.Update(grupoCadastrado);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Grupo não existe");
            };

            return NotFound("Grupo já tem gerenciados, favor deletá-lo antes");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("deletar-usuario-gerencial/{idGrupo}")]
    public IActionResult DeletarUsuarioGerencia([FromRoute] int idGrupo)
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

    //Métodos para ver usuários em um grupo
    //GET - listar os grupos
    [HttpGet]
    [Route("listar/listar-operacional/{id}")]
    public IActionResult ListarUsuariosOperacionaisDoGrupo([FromRoute] int id){
        try
        {
            Grupo? grupoCadastrado =
                _context.Grupos.FirstOrDefault(x => x.GrupoId == id);

            if(grupoCadastrado != null && grupoCadastrado.UsuariosOperacionais != null){
                ICollection<UsuarioOperacional> usuarios = grupoCadastrado.UsuariosOperacionais;
                return Ok(usuarios);
            }else{
                return NotFound("Grupo/Usuário não encontrado");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
