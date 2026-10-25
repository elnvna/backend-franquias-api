using System.Net;
using System.Net.Http.Json;
using Franquias.Api.DTOs;

namespace Franquias.Api.Services;

public class ViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ViaCepResponseDto?> ConsultarCepAsync(string cep)
    {
        cep = new string(cep.Where(char.IsDigit).ToArray());

        if (cep.Length != 8)
            return null;

        var response = await _httpClient.GetAsync(
            $"https://viacep.com.br/ws/{cep}/json/");

        if (response.StatusCode == HttpStatusCode.BadRequest)
            return null;

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Erro ao consultar a API ViaCEP. Status: {response.StatusCode}");

        var resultado = await response.Content
            .ReadFromJsonAsync<ViaCepResponseDto>();

        if (resultado == null || resultado.Erro)
            return null;

        return resultado;
    }
}