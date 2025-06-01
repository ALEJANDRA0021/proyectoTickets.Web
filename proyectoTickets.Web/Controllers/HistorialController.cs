using Microsoft.AspNetCore.Mvc;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{
    public class HistorialController : Controller
    {
        private readonly HistorialService _historialService;

        public HistorialController(HistorialService historialService)
        {
            _historialService = historialService;
        }

        public async Task<IActionResult> Index()
        {
            var historiales = await _historialService.GetHistorialesAsync();
            return View(historiales);
        }

        public async Task<IActionResult> Details(int id)
        {
            var historial = await _historialService.GetHistorialAsync(id);
            if (historial == null) return NotFound();
            return View(historial);
        }
    }
}