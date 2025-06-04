using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace proyectoTickets.Web.Filters
{
	public class EmpleadoeOnlyAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var userType = context.HttpContext.Session.GetString("UserType");

			if (string.IsNullOrEmpty(userType) || userType.ToLower() != "empleado")
			{
				context.Result = new RedirectToActionResult("Index", "Empleado", null);
			}

			base.OnActionExecuting(context);
		}
	}
}
