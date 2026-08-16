namespace Franquias.Api.Models;

public enum PerfilUsuario
{
    AdminFranqueadora = 1,
    GestorUnidade = 2,
    Operador = 3
}

public enum StatusChamado
{
    Aberto = 1,
    EmAndamento = 2,
    Fechado = 3
}

public enum PrioridadeChamado
{
    Baixa = 1,
    Media = 2,
    Alta = 3
}