using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
