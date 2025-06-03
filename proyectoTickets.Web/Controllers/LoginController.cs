using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using proyectoTickets.Web.Models;
using proyectoTickets.Web.Services;
using System.Net.Http;

namespace proyectoTickets.Web.Controllers
{
	public class LoginController : Controller
	{
		private UsuarioService _usuarioService;

		public LoginController(UsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}
		public IActionResult Index()
		{
			var model = new LoginModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginModel model)
		{
			if (!ModelState.IsValid)
				return View(model);			 

			var response = await _usuarioService.LoginUsuarioAsync(model);				

			if (response!=null)
			{
				// Save user info in session
				HttpContext.Session.SetString("UserEmail", response.Email);
				HttpContext.Session.SetString("UserName", response.Nombre);
				HttpContext.Session.SetString("UserType", response.TipoUsuario);
				if(response.TipoUsuario == "admin")
				{
					return RedirectToAction("Index", "Admin");
				}
				
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Invalid email or password.");
			return View(model);
		}

		[HttpGet]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Login");
		}
	}
}
