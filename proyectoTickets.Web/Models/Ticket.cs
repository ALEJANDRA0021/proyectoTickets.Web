namespace proyectoTickets.Web.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; } = "Abierto";
        public string Prioridad { get; set; } = "Media";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
        public int ClienteId { get; set; }
        public int CategoriaId { get; set; }
        public int? EmpleadoAsignadoId { get; set; }
    }
}
