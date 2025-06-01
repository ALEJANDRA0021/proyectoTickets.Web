namespace proyectoTickets.Web.Models
{
    public class ComentarioTicket
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public int TicketId { get; set; }
        public int UsuarioId { get; set; }
    }
}
