using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;
    public UsuarioService(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IMapper mapper,
        TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task CadastrarUsuario(CreateUsuarioDto usuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
        var result = await _userManager.CreateAsync(usuario, usuarioDto.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário");
        }
    }

    public async Task<string> Login(LoginUsuarioDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao logar");
        }

        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(x => x.NormalizedUserName == loginDto.Username.ToUpper());
        if (usuario == null)
        {
            throw new ApplicationException("Falha ao logar");
        }

        var token = _tokenService.GenerateToken(usuario);

        return token;
    }
}
