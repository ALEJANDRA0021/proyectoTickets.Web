using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoTickets.Web.Filters;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{

    [EmpleadoeOnly]
    public class EmpleadosController : Controller
    {
        private readonly TicketService _ticketService;
        private readonly CategoriaService _categoriaService;
        private readonly ComentarioService _comentarioService;

        public EmpleadosController(TicketService ticketService, CategoriaService categoriaService, ComentarioService comentarioService)
        {
            _ticketService = ticketService;
            _categoriaService = categoriaService;
           _comentarioService = comentarioService;
        }
        public IActionResult Index()
        {
            var tickets = _ticketService.GetTicketsAsync();
            var clientId = HttpContext.Session.GetInt32("UserId");
            var model = tickets.Result.Where(t => t.EmpleadoAsignadoId == clientId).ToList();
            return View(model);
        }

        public async Task<IActionResult> VerTicket(int id)
        {
            var ticket = await _ticketService.GetTicketAsync(id);
            var comentarios = await _comentarioService.GetComentariosAsync(id); 
            var model = new ComentariosTicketModel
            {
                Ticket = ticket??new Ticket(),
                Comentarios = comentarios.Where(c => c.TicketId == id).ToList()
            };  
          
            return View(model);
        }
    }
}