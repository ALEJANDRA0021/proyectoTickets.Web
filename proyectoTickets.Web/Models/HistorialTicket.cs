namespace proyectoTickets.Web.Models
{
    public class HistorialTicket
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; } // "Abierto", "En Proceso", "Cerrado"
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public int TicketId { get; set; }
        public int UsuarioId { get; set; }
    }
}
