using Microsoft.AspNetCore.Mvc;
using proyectoTickets.Web.Filters;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;

namespace proyectoTickets.Web.Controllers
{
	[AdminOnly]
	public class AdminController : Controller
	{
		private readonly UsuarioService _usuarioService;

		public AdminController(UsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}
		public IActionResult Index()
		{
			return View();
		}
		
	}
}
