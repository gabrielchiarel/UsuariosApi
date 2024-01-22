using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models;

public class Usuario : IdentityUser
{
    public DateTime DataNascimento { get; set; }

    public Usuario(): base() { }
}
