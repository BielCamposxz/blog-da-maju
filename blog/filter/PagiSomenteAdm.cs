using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace blog.filter
{
    public class PagiSomenteAdm : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Bloqueio" } });
                return;
            }

            var usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

            if (usuario == null || usuario.Id == 0)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Bloqueio" } });
                return;
            }

            if (usuario.isAdmin == false)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Bloqueio" } });
                return;
            }

        }
    }
}
