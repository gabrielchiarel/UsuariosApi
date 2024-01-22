using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    
    UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario([FromBody] CreateUsuarioDto usuarioDto)
    {
        await _usuarioService.CadastrarUsuario(usuarioDto);
        return Ok("Usuário cadastrado"); 
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioDto loginDto)
    {
        var token = await _usuarioService.Login(loginDto);
        return Ok(token);
    }
}
