using Microsoft.AspNetCore.Mvc;
using API.Data;
namespace API;
using System.Security.Cryptography;
using System.Text;


public class UsuarioController : ControllerBase
{
    private readonly AppDataContext _context;
    public UsuarioController(AppDataContext context){
        _context = context;
    }

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

    // POST: Exclusivo para usuário Admin
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
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

    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            Usuario? usuarioCadastro = _context.Usuarios.Find(id);
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

    [HttpPut]
    [Route("alterar/{id}")]
    public IActionResult Alterar([FromRoute] int id,
        [FromBody] Usuario usuario)
    {
        try
        {
            //Expressões lambda
            Usuario? usuarioCadastro =
                _context.Usuarios.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro != null)
            {
                usuarioCadastro.Nome = usuario.Nome;
                usuarioCadastro.Login = usuario.Login;
                usuarioCadastro.Senha = usuario.Senha;
                usuarioCadastro.DataNascimento = usuario.DataNascimento;
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

    //Metodo Listar Tarefa de usuario, chamando pelo ID
    [HttpGet]
    [Route("listarTarefasUsuario/{id}")]
    public IActionResult ListarTarefasDoUsuario([FromRoute]int id, [FromBody] Usuario usuario)
    {
        try
        {
            // Primeiro, verifique se o usuário existe
             Usuario? usuarioCadastro = _context.Usuarios.FirstOrDefault(x => x.UsuarioId == id);

            if (usuarioCadastro == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Em seguida, recupere as tarefas do usuário
            var tarefasDoUsuario = usuario.Tarefas;

            return Ok(tarefasDoUsuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //----
    //Metodo Listar Todas as Tarefas - Usuários Admin e Gerencial; Buscar do Route das Tarefas - GET
    //Metodo Criar Novas Tarefas - Todos os Usuários; Buscar do Route das Tarefas - POST
    //Metodo Alterar Tarefas - Todos os Usuarios; Buscar do Route das Tarefas - PUT
    //Metodo Deletar - Apenas o Admin; Buscar do Route das Tarefas - DELETE
    //Método de adicionar comentário em tarefa - Apenas Gerencial e Admin; Buscar do Route das Tarefas - PUT
    //----


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

