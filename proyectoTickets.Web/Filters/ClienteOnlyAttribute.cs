using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace proyectoTickets.Web.Filters
{
	public class ClienteOnlyAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var userType = context.HttpContext.Session.GetString("UserType");

			if (string.IsNullOrEmpty(userType) || userType.ToLower() != "cliente")
			{
				context.Result = new RedirectToActionResult("Index", "Login", null);
			}

			base.OnActionExecuting(context);
		}
	}
}
