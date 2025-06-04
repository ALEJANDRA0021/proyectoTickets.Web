using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoTickets.Web.Filters;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;
using System.Net.Sockets;
using System.Reflection;

namespace proyectoTickets.Web.Controllers
{

    [EmpleadoeOnly]
    public class EmpleadosController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly TicketService _ticketService;
        private readonly CategoriaService _categoriaService;
        private readonly ComentarioService _comentarioService;
        private readonly IEmailService _emailService;

        public EmpleadosController(UsuarioService usuarioService, TicketService ticketService, CategoriaService categoriaService, ComentarioService comentarioService, IEmailService emailService)
        {
            _usuarioService = usuarioService;
            _ticketService = ticketService;
            _categoriaService = categoriaService;
           _comentarioService = comentarioService;
            _emailService = emailService;
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
            var usuario = await _usuarioService.GetUsuarioAsync(ticket?.EmpleadoAsignadoId??0);
            var cliente = await _usuarioService.GetUsuarioAsync(ticket?.ClienteId ?? 0);
            await _emailService.SendEmailAsync(
                   to: $"{cliente?.Email}",  
                   subject: $"Se ha cambiado el estado al ticket #{ticketId}",
                   body: $"<p>El usuario <strong>{usuario?.Nombre}</strong> ha cambiado el estatus a {nuevoEstado} en tu ticket con titulo {ticket?.Titulo}.</p>"
               );

            return RedirectToAction("VerTicket", new { id = ticketId });
        }
        public   IActionResult AgregarComentario(int ticketId)
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
            var ticket = await _ticketService.GetTicketAsync(model.TicketId);
            var usuario = await _usuarioService.GetUsuarioAsync(model.UsuarioId);
            var cliente = await _usuarioService.GetUsuarioAsync(ticket?.ClienteId??0);
            await _emailService.SendEmailAsync(
                   to: $"{cliente?.Email}",  // You can fetch this from the ticket/usuario
                   subject: $"Se ha agregado un comentario al ticket #{model.TicketId}",
                   body: $"<p>El usuario <strong>{usuario?.Nombre}</strong> ha agregado un comentario en tu ticket con titulo {ticket?.Titulo}.</p>"
               );
            return RedirectToAction("VerTicket", new { id=model.TicketId});
        }

    }
}