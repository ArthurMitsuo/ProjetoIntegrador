using Microsoft.AspNetCore.Mvc;
using API.Data;
namespace API;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly AppDataContext _context;
    public UsuarioController(AppDataContext context){
        _context = context;
    }

    //GET - listam todos os usuários
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Usuario> usuario = _context.Usuarios.ToList();
            return Ok(usuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //listam todos os usuários OPERACIONAIS
    [HttpGet]
    [Route("operacional/listar")]
    public IActionResult ListarOperacional(){
        try
        {
            List<UsuarioOperacional> userOperacional = _context.UsuariosOperacionais
                .Include(g => g.Grupo)
                .Include(c => c.Cargo)
                .ToList();

            //List<UsuarioOperacional> usuario = _context.UsuariosOperacionais.ToList();
            return Ok(userOperacional);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //listam todos os usuários GERENCIAIS
    [HttpGet]
    [Route("gerencial/listar")]
    public IActionResult ListarGerencial(){
        try
        {
            List<UsuarioGerencial> userGerencial = _context.UsuariosGerenciais
                .Include(g => g.Grupo)
                .Include(c => c.Cargo)
                .ToList();

            //List<UsuarioGerencial> usuario = _context.UsuariosGerenciais.ToList();
            return Ok(userGerencial);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //listam todos os usuários ADMIN
    [HttpGet]
    [Route("admin/listar")]
    public IActionResult ListarAdmin(){
        try
        {
            List<UsuarioAdmin> usuario = _context.UsuariosAdmin.ToList();
            return Ok(usuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: Exclusivo para usuário Admin
    //Cadastrar Operacional
    [HttpPost]
    [Route("operacional/cadastrar")]
    public IActionResult CadastrarOperacional([FromBody] UsuarioOperacional usuario)
    {
        try
        {
            string? usuarioSenha = usuario.Senha;

            // Gerar um salt aleatório (um valor único para cada usuário)
            byte[] salt = GenerateSalt();

            // Gerar o hash da senha com o salt
            string senhaHashed = HashPassword(usuarioSenha, salt);

            usuario.Senha = senhaHashed;

            _context.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //Cadastrar Gerencial
    [HttpPost]
    [Route("gerencial/cadastrar")]
    public IActionResult CadastrarGerencial([FromBody] UsuarioGerencial usuario)
    {
        try
        {
            string? usuarioSenha = usuario.Senha;

            // Gerar um salt aleatório (um valor único para cada usuário)
            byte[] salt = GenerateSalt();

            // Gerar o hash da senha com o salt
            string senhaHashed = HashPassword(usuarioSenha, salt);

            usuario.Senha = senhaHashed;

            _context.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //Cadastrar Admin
    [HttpPost]
    [Route("admin/cadastrar")]
    public IActionResult CadastrarAdmin([FromBody] UsuarioAdmin usuario)
    {
        try
        {
            string? usuarioSenha = usuario.Senha;

            // Gerar um salt aleatório (um valor único para cada usuário)
            byte[] salt = GenerateSalt();

            // Gerar o hash da senha com o salt
            string senhaHashed = HashPassword(usuarioSenha, salt);

            usuario.Senha = senhaHashed;

            _context.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //DELETE - apenas Admin
    //Operacional
    [HttpDelete]
    [Route("operacional/deletar/{id}")]
    public IActionResult DeletarOperacional([FromRoute] int id)
    {
        try
        {
            UsuarioOperacional? usuarioCadastro = _context.UsuariosOperacionais.Find(id);
            if (usuarioCadastro != null)
            {
                _context.Usuarios.Remove(usuarioCadastro);
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
    //Gerencial
    [HttpDelete]
    [Route("gerencial/deletar/{id}")]
    public IActionResult DeletarGerencial([FromRoute] int id)
    {
        try
        {
            UsuarioGerencial? usuarioCadastro = _context.UsuariosGerenciais.Find(id);
            if (usuarioCadastro != null)
            {
                _context.Usuarios.Remove(usuarioCadastro);
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
    //Admin
    [HttpDelete]
    [Route("admin/deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            UsuarioAdmin? usuarioCadastro = _context.UsuariosAdmin.Find(id);
            if (usuarioCadastro != null)
            {
                _context.Usuarios.Remove(usuarioCadastro);
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

    //PUT - para o próprio usuário, apenas ele; Para admin, todos
    //Operacional
    [HttpPut]
    [Route("operacional/alterar/{id}")]
    public IActionResult AlterarOperacional([FromRoute] int id,
        [FromBody] Usuario usuario)
    {
        try
        {
            //Expressões lambda
            UsuarioOperacional? usuarioCadastro =
                _context.UsuariosOperacionais.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro != null)
            {
                usuarioCadastro.Nome = usuario.Nome;
                usuarioCadastro.Login = usuario.Login;
                usuarioCadastro.Senha = usuario.Senha;
                usuarioCadastro.DataNascimento = usuario.DataNascimento;
                _context.UsuariosOperacionais.Update(usuarioCadastro);
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
    //Gerencial
    [HttpPut]
    [Route("gerencial/alterar/{id}")]
    public IActionResult AlterarGerencial([FromRoute] int id,
        [FromBody] Usuario usuario)
    {
        try
        {
            //Expressões lambda
            UsuarioGerencial? usuarioCadastro =
                _context.UsuariosGerenciais.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro != null)
            {
                usuarioCadastro.Nome = usuario.Nome;
                usuarioCadastro.Login = usuario.Login;
                usuarioCadastro.Senha = usuario.Senha;
                usuarioCadastro.DataNascimento = usuario.DataNascimento;
                _context.UsuariosGerenciais.Update(usuarioCadastro);
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
    //Admin
    [HttpPut]
    [Route("admin/alterar/{id}")]
    public IActionResult AlterarAdmin([FromRoute] int id,
        [FromBody] Usuario usuario)
    {
        try
        {
            //Expressões lambda
            UsuarioAdmin? usuarioCadastro =
                _context.UsuariosAdmin.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro != null)
            {
                usuarioCadastro.Nome = usuario.Nome;
                usuarioCadastro.Login = usuario.Login;
                usuarioCadastro.Senha = usuario.Senha;
                usuarioCadastro.DataNascimento = usuario.DataNascimento;
                _context.UsuariosAdmin.Update(usuarioCadastro);
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

     //PUT - alterar cargo - para o próprio usuário, apenas ele; Para admin, todos
    //Operacional
    [HttpPut]
    [Route("operacional/alterar-cargo/{idUsuario}/{idCargo}")]
    public async Task<IActionResult> AlterarCargoOperacionalAsync([FromRoute] int idUsuario, [FromRoute] int idCargo)
    {
        try
        {
            //Expressões lambda
            Cargo? cargo = await _context.Cargos.FirstOrDefaultAsync(x => x.CargoId == idCargo);

            UsuarioOperacional? usuarioCadastro = await _context.UsuariosOperacionais
                .Include(c => c.Cargo)
                .FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);  

            if (usuarioCadastro == null){
                return NotFound("Usuário não encontrado");
            }
            if(cargo == null){
                return NotFound("Cargo Inexistente/Não Encontrado");
            }
            
            usuarioCadastro.Cargo = cargo;
            _context.UsuariosOperacionais.Update(usuarioCadastro);
            _ = _context.SaveChangesAsync();
            return Ok();
  
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //Gerencial
    [HttpPut]
    [Route("gerencial/alterar-cargo/{idUsuario}/{idCargo}")]
    public async Task<IActionResult> AlterarCargoGerencial([FromRoute] int idUsuario, [FromRoute] int idCargo)
    {
        try
        {
            //Expressões lambda
            Cargo? cargo = await _context.Cargos.FirstOrDefaultAsync(x => x.CargoId == idCargo);

            UsuarioGerencial? usuarioCadastro = await _context.UsuariosGerenciais
                .Include(c => c.Cargo)
                .FirstOrDefaultAsync(x => x.UsuarioId == idUsuario);  

            if (usuarioCadastro == null){
                return NotFound("Usuário não encontrado");
            }
            if(cargo == null){
                return NotFound("Cargo Inexistente/Não Encontrado");
            }
            
            usuarioCadastro.Cargo = cargo;
            _context.UsuariosGerenciais.Update(usuarioCadastro);
            _ = _context.SaveChangesAsync();
            return Ok();
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Metodo Listar Tarefa de usuario Operacional, chamando pelo ID do Usuario
    [HttpGet]
    [Route("operacional/listar-tarefa/{id}")]
    public IActionResult ListarTarefasDoUsuarioOperacional([FromRoute]int id)
    {
        try
        {
            // Primeiro, verifique se o usuário existe
             UsuarioOperacional? usuarioCadastro = _context.UsuariosOperacionais.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Em seguida, recupere as tarefas do usuário
            var tarefasDoUsuario = usuarioCadastro.Tarefa;

            return Ok(tarefasDoUsuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //Metodo Listar Tarefa de usuario Gerencial, chamando pelo ID do Usuario
    [HttpGet]
    [Route("gerencial/listar-tarefa/{id}")]
    public IActionResult ListarTarefasDoUsuarioGerencial([FromRoute]int id)
    {
        try
        {
            // Primeiro, verifique se o usuário existe
             UsuarioGerencial? usuarioCadastro = _context.UsuariosGerenciais.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Em seguida, recupere as tarefas do usuário
            var tarefasDoUsuario = usuarioCadastro.Tarefa;

            return Ok(tarefasDoUsuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //----TAREFAS---
    //Metodo Listar Todas as Tarefas - Usuários Admin e Gerencial; Buscar do Route das Tarefas - GET
    //Metodo Criar Novas Tarefas - Todos os Usuários; Buscar do Route das Tarefas - POST
    //Metodo Alterar Tarefas - Todos os Usuarios; Buscar do Route das Tarefas - PUT
    //Metodo Deletar - Apenas o Admin; Buscar do Route das Tarefas - DELETE
    //Método de adicionar comentário em tarefa - Apenas Gerencial e Admin; Buscar do Route das Tarefas - PUT
    //-------------

    //----GRUPOS---
    //Metodo Listar Todas os Grupos - Usuários Admin e Gerencial; Buscar do Route dos Grupos - GET
    //Metodo Criar Novos Grupos - Todos os Usuários; Buscar do Route dos Grupos - POST
    //Metodo Alterar Grupos - Apenas o Admin; Buscar do Route dos Grupos - PUT
    //Metodo Deletar - Apenas o Admin; Buscar do Route das Grupos - DELETE
    //-------------


    //------Sessão para criptografia de senha de usuário--------
    // Gera um salt aleatório
    public static byte[] GenerateSalt()
    {   
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    // Gera o hash da senha usando o salt
    public static string HashPassword(string? senha, byte[] salt)
    {
        if (senha == null)
        {
            throw new ArgumentNullException(nameof(senha), "A senha não pode ser nula.");
        }

        using (var sha256 = SHA256.Create())
        {
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
            byte[] senhaComSalt = new byte[senhaBytes.Length + salt.Length];

            for (int i = 0; i < senhaBytes.Length; i++)
            {
                senhaComSalt[i] = senhaBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                senhaComSalt[senhaBytes.Length + i] = salt[i];
            }

            byte[] hashedPassword = sha256.ComputeHash(senhaComSalt);
            return Convert.ToBase64String(hashedPassword);
        }
    }

    // Verifica se uma senha fornecida corresponde ao hash armazenado
    public static bool VerifyPassword(string senha, byte[] salt, string hash)
    {
        string senhaHashed = HashPassword(senha, salt);
        return senhaHashed == hash;
    }
    //-------------------------FIM---------------------------
}

