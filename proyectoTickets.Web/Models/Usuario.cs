namespace proyectoTickets.Web.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public  string Nombre { get; set; }
        public  string Email { get; set; }
        public  string PasswordHash { get; set; }
        public  string TipoUsuario { get; set; } // "empleado" o "cliente"
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    }
}
