﻿using proyectoTickets.Web.Models;

namespace proyectoTickets.Web.Services
{
    public class ComentarioService
    {
        private readonly HttpClient _httpClient;

        public ComentarioService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("WebApi");
        }

        public async Task<List<ComentarioTicket>> GetComentariosAsync(int ticketId)
        {
            return await _httpClient.GetFromJsonAsync<List<ComentarioTicket>>("api/ComentariosTicket?ticketId=" +ticketId) ?? new List<ComentarioTicket>();
        }

        public async Task<ComentarioTicket?> GetComentarioAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ComentarioTicket>($"api/ComentariosTicket/{id}");
        }

        public async Task<bool> CreateComentarioAsync(ComentarioTicket comentario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ComentariosTicket", comentario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateComentarioAsync(int id, ComentarioTicket comentario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ComentariosTicket/{id}", comentario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteComentarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ComentariosTicket/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}