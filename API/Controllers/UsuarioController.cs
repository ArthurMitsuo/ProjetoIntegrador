﻿using Microsoft.AspNetCore.Mvc;
using API.Data;
namespace API;

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

    // POST: api/categoria/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        try
        {
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
}