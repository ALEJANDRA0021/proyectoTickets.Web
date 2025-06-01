using proyectoTickets.Web.Models;

namespace proyectoTickets.Web.Services
{
    public class CategoriaService
    {
        private readonly HttpClient _httpClient;

        public CategoriaService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("WebApi");
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Categoria>>("api/Categorias") ?? new List<Categoria>();
        }

        public async Task<Categoria?> GetCategoriaAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Categoria>($"api/Categorias/{id}");
        }

        public async Task<bool> CreateCategoriaAsync(Categoria categoria)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Categorias", categoria);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoriaAsync(int id, Categoria categoria)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Categorias/{id}", categoria);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Categorias/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}