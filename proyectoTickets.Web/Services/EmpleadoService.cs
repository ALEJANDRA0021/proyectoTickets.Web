using proyectoTickets.Web.Models;

namespace proyectoTickets.Web.Services
{
    public class EmpleadoService
    {
        private readonly HttpClient _httpClient;

        public EmpleadoService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("WebApi");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Empleado>>("api/Empleados") ?? new List<Empleado>();
        }

        public async Task<Empleado?> GetEmpleadoAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Empleado>($"api/Empleados/{id}");
        }

        public async Task<bool> CreateEmpleadoAsync(Empleado empleado)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Empleados", empleado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEmpleadoAsync(int id, Empleado empleado)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Empleados/{id}", empleado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Empleados/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}