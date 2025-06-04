namespace proyectoTickets.Web.Models
{
    public class ComentariosTicketModel
    {
        public Ticket Ticket { get; set; }
        public List<ComentarioTicket> Comentarios { get; set; }
        public List<string> Estados { get; set; }

    }
}
