using Microsoft.AspNetCore.Mvc;
using proyectoTickets.Web.Filters;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{

    [AdminOnly]
    public class EmpleadosController : Controller
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadosController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        public async Task<IActionResult> Index()
        {
            var empleados = await _empleadoService.GetEmpleadosAsync();
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            var empleado = await _empleadoService.GetEmpleadoAsync(id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (!ModelState.IsValid) return View(empleado);
            await _empleadoService.CreateEmpleadoAsync(empleado);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var empleado = await _empleadoService.GetEmpleadoAsync(id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Empleado empleado)
        {
            if (!ModelState.IsValid) return View(empleado);
            await _empleadoService.UpdateEmpleadoAsync(id, empleado);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _empleadoService.GetEmpleadoAsync(id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empleadoService.DeleteEmpleadoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}