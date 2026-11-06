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
        // Send "courseNumber" (not "CourseNumber"), the same names RestDb uses.
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
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

    // GET courses -> read a list
    public async Task<List<T>> GetValuesAsync(string resource)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(resource);
        response.EnsureSuccessStatusCode();

        List<T>? items = await response.Content.ReadFromJsonAsync<List<T>>(_jsonOptions);
        return items ?? [];
    }

    // POST courses -> create a new record (the item travels as JSON in the request body)
    public async Task PostAsync(string resource, T item)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(resource, item, _jsonOptions);
        response.EnsureSuccessStatusCode();
    }

    // PUT courses/{id} -> change an existing record
    public async Task PutAsync(string resource, string id, T item)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{resource}/{id}", item, _jsonOptions);
        response.EnsureSuccessStatusCode();
    }

    // DELETE courses/{id} -> remove a record
    public async Task DeleteAsync(string resource, string id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{resource}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
