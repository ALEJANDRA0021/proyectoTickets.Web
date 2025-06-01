using proyectoTickets.Web.Models;

namespace proyectoTickets.Web.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("WebApi");
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>("api/Tickets") ?? new List<Ticket>();
        }

        public async Task<Ticket?> GetTicketAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ticket>($"api/Tickets/{id}");
        }

        public async Task<bool> CreateTicketAsync(Ticket ticket)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tickets", ticket);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicketAsync(int id, Ticket ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Tickets/{id}", ticket);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tickets/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}