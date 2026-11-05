using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Client.Services;

public class HttpService<T> : IHttpService<T> where T : class
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public HttpService(IConfiguration config)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(config["RestDb:BaseUrl"]!)
        };
        _httpClient.DefaultRequestHeaders.Add("x-apikey", config["RestDb:ApiKey"]);
    }

    public async Task<List<T>> GetValuesAsync(string resource)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(resource);
        response.EnsureSuccessStatusCode();

        List<T>? items = await response.Content.ReadFromJsonAsync<List<T>>(_jsonOptions);
        return items ?? [];
    }
}
