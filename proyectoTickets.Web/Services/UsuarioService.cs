using proyectoTickets.Web.Models;
using System.Security.Cryptography;
using System.Text;

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

        public  string EncryptToMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<bool> CreateUsuarioAsync(Usuario usuario)
        {
            usuario.PasswordHash = EncryptToMD5(usuario.PasswordHash); 
            var response = await _httpClient.PostAsJsonAsync("api/Usuarios", usuario);
            usuario.FechaCreacion = DateTime.Now;    
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