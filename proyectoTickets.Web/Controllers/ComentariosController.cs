using Microsoft.AspNetCore.Mvc;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ComentarioService _comentarioService;

        public ComentariosController(ComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        public async Task<IActionResult> Index()
        {
            var comentarios = await _comentarioService.GetComentariosAsync();
            return View(comentarios);
        }

        public async Task<IActionResult> Details(int id)
        {
            var comentario = await _comentarioService.GetComentarioAsync(id);
            if (comentario == null) return NotFound();
            return View(comentario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComentarioTicket comentario)
        {
            if (!ModelState.IsValid) return View(comentario);
            await _comentarioService.CreateComentarioAsync(comentario);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comentario = await _comentarioService.GetComentarioAsync(id);
            if (comentario == null) return NotFound();
            return View(comentario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ComentarioTicket comentario)
        {
            if (!ModelState.IsValid) return View(comentario);
            await _comentarioService.UpdateComentarioAsync(id, comentario);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comentario = await _comentarioService.GetComentarioAsync(id);
            if (comentario == null) return NotFound();
            return View(comentario);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _comentarioService.DeleteComentarioAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}