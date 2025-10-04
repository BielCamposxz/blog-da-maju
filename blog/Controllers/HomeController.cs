using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly BancoContext _context;

        public HomeController(BancoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var posts = _context.Post.ToList();
            foreach (var post in posts)
            {
                string sessionKey = $"liked_{post.Id}";
                post.LikedByCurrentUser = HttpContext.Session.GetString(sessionKey) == "1";
            }
            var perfil = _context.Perfil.FirstOrDefault() ?? new perfilModel { Id = 1, NomeUsuario = "Usuário" };
            var perfilPostModel = new PerfilPostModel
            {
                Posts = posts,
                Perfil = perfil
            };
            return View(perfilPostModel);
        }

        public IActionResult Exibir(int id)
        {
            var foto = _context.Post.Find(id);
            return File(foto.Imagem, foto.ContentType);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
