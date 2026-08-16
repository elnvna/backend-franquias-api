namespace Franquias.Api.DTOs;

public record LoginDto(string Email, string Senha);

public record TokenResponseDto(string Token, string Usuario, string Perfil);