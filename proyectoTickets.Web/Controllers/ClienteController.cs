using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoTickets.Web.Filters;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{
    [ClienteOnly]
    public class ClienteController : Controller
    {
        private readonly TicketService _ticketService;
        private readonly CategoriaService _categoriaService;

        public ClienteController(TicketService ticketService, CategoriaService categoriaService)
        {
           _ticketService = ticketService;
            _categoriaService = categoriaService;
        }
        public IActionResult Index()
        {
            var tickets = _ticketService.GetTicketsAsync();
            var clientId = HttpContext.Session.GetInt32("UserId");
            var model = tickets.Result.Where(t => t.ClienteId == clientId).ToList();
            return View(model);
        }
        public async Task<IActionResult> CreateTicket()
        {
            var model = new Ticket
            {
                FechaCreacion = DateTime.Now,
                Estado = "Abierto",
                ClienteId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault()  
            };

            var prioridades = await _categoriaService.GetCategoriasAsync(); 
            
            ViewBag.Prioridades = new SelectList(prioridades, "Id", "Nombre");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = await _categoriaService.GetCategoriasAsync();
                return View(ticket);
            }

            await _ticketService.CreateTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

    }
}
