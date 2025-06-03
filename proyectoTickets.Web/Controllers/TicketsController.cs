using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proyectoTickets.Web.Filters;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{

    [AdminOnly]
    public class TicketsController : Controller
    {
        private readonly TicketService _ticketService;
        private readonly CategoriaService _categoriaService;
        private readonly UsuarioService _usuarioService;

        public TicketsController(
            TicketService ticketService,
            CategoriaService categoriaService,
            UsuarioService usuarioService)
        {
            _ticketService = ticketService;
            _categoriaService = categoriaService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetTicketsAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        public async Task<IActionResult> Create()
        {
            var model = new Ticket
            {
                FechaCreacion = DateTime.Now,
                Estado = "Abierto"
            };
            var usuarios = await _usuarioService.GetUsuariosAsync();
            var prioridades = await _categoriaService.GetCategoriasAsync();
            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
            ViewBag.Prioridades = new SelectList(prioridades,"Id","Nombre"); 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = await _categoriaService.GetCategoriasAsync();
                return View(ticket);
            }

            await _ticketService.CreateTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketAsync(id);
            if (ticket == null) return NotFound();

           var categorias = await _categoriaService.GetCategoriasAsync();
            var empleados = await _usuarioService.GetEmpleadosAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            ViewBag.Empleados = new SelectList(empleados.Where(x => x.TipoUsuario=="empleado"), "Id", "Nombre");

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = await _categoriaService.GetCategoriasAsync();
                ViewBag.Empleados = await _usuarioService.GetEmpleadosAsync();
                return View(ticket);
            }

            await _ticketService.UpdateTicketAsync(id, ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}