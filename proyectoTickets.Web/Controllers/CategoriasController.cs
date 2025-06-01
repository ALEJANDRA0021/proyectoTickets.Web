using Microsoft.AspNetCore.Mvc;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.GetCategoriasAsync();
            return View(categorias);
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaService.GetCategoriaAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);
            await _categoriaService.CreateCategoriaAsync(categoria);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.GetCategoriaAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);
            await _categoriaService.UpdateCategoriaAsync(id, categoria);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetCategoriaAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaService.DeleteCategoriaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}