using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Franquias.Api.Data;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Franquias.Api.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // Método para autenticação e geração do Token
    public async Task<TokenResponseDto?> AutenticarAsync(LoginDto login)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == login.Email && u.Ativo);

        if (usuario == null || !VerificarSenha(login.Senha, usuario.SenhaHash))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "ChaveSecretaSuperSeguraParaAFranquiaApi2026!");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                new Claim("UnidadeId", usuario.UnidadeId?.ToString() ?? "")
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenResponseDto(tokenHandler.WriteToken(token), usuario.Nome, usuario.Perfil.ToString());
    }

    // Criptografia de senha usando SHA256 simples para fins acadêmicos
    public static string GerarHashSenha(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(bytes);
    }

    public static bool VerificarSenha(string senhaDigitada, string hashSalvo)
    {
        return GerarHashSenha(senhaDigitada) == hashSalvo;
    }
}