namespace proyectoTickets.Web.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Prioridad { get; set; } // "Baja", "Media", "Alta"
    }
}
