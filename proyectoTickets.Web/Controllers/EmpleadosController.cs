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
                Comentarios = comentarios.Where(c => c.TicketId == id).ToList(),
                Estados  = new List<string> { "Abierto", "En Proceso", "Cerrado" }
            };  
          
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ActualizarEstado(int ticketId, string nuevoEstado)
        {
            var ticket = await _ticketService.GetTicketAsync(ticketId);
            if (ticket != null)
            {
                ticket.Estado = nuevoEstado;
                await _ticketService.UpdateTicketAsync(ticketId,ticket);
            }

            return RedirectToAction("VerTicket", new { id = ticketId });
        }
        public  IActionResult AgregarComentario(int ticketId)
        {                      
            var model = new ComentarioTicket
            {
               TicketId = ticketId, 
               UsuarioId = HttpContext.Session.GetInt32("UserId") ?? 0,
               Fecha= DateTime.Now
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarComentario(ComentarioTicket model)
        {
            await _comentarioService.CreateComentarioAsync(model);
            return RedirectToAction("VerTicket", new { id=model.TicketId});
        }

    }
}