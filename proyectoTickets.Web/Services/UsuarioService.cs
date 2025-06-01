using proyectoTickets.Web.Models;

namespace proyectoTickets.Web.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("WebApi");
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("api/Usuarios") ?? new List<Usuario>();
        }

        public async Task<List<Usuario>> GetEmpleadosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("api/Usuarios/empleados") ?? new List<Usuario>();
        }

        public async Task<Usuario?> GetUsuarioAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"api/Usuarios/{id}");
        }

        public async Task<bool> CreateUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuarios", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUsuarioAsync(int id, Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Usuarios/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Usuarios/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}