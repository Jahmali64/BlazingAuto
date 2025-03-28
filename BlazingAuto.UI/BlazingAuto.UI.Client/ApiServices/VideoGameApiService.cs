using System.Net.Http.Json;
using BlazingAuto.Application.Services.VideoGames.Dtos;

namespace BlazingAuto.UI.Client.ApiServices;

public sealed class VideoGameApiService {
    private readonly HttpClient _httpClient;

    public VideoGameApiService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<VideoGameDto>> GetAllAsync() {
        return await _httpClient.GetFromJsonAsync<List<VideoGameDto>>("videogame") ?? [];
    }

    public async Task<VideoGameDto?> GetByIdAsync(int id) {
        return await _httpClient.GetFromJsonAsync<VideoGameDto>($"videogame/{id}");
    }

    public async Task<VideoGameDto?> AddAsync(CreateVideoGameDto dto) {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("videogame", dto);
        return await response.Content.ReadFromJsonAsync<VideoGameDto>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateVideoGameDto dto) {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"videogame/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id) {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"videogame/{id}");
        return response.IsSuccessStatusCode;
    }
}
