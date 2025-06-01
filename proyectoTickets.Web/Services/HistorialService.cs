using proyectoTickets.Web.Models;

namespace proyectoTickets.Web.Services
{
    public class HistorialService
    {
        private readonly HttpClient _httpClient;

        public HistorialService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("WebApi");
        }

        public async Task<List<HistorialTicket>> GetHistorialesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<HistorialTicket>>("api/Historial") ?? new List<HistorialTicket>();
        }

        public async Task<HistorialTicket?> GetHistorialAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<HistorialTicket>($"api/Historial/{id}");
        }
    }
}