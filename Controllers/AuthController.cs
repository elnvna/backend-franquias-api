using Franquias.Api.DTOs;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var resultado = await _authService.AutenticarAsync(dto);
        if (resultado == null)
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

        return Ok(resultado);
    }
}