using blog.Data;
using blog.Helper;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly BancoContext _context;
        private readonly IsessaoDoUsuario _sessaoDoUsuario;

        public HomeController(BancoContext context, IsessaoDoUsuario sessaoDoUsuario)
        {
            _context = context;
            _sessaoDoUsuario = sessaoDoUsuario;
        }
        public IActionResult Index()
        {
            var posts = _context.Post.ToList();

            var usuario = _sessaoDoUsuario.BuscarSessaoDoUsuario();

            foreach (var post in posts)
            {
                if (usuario != null)
                {
                    // Verifica no banco se o usuário curtiu este post
                    post.LikedByCurrentUser = _context.LikesModel
                        .Any(l => l.PostId == post.Id && l.UsuarioId == usuario.Id);
                }
                else
                {
                    post.LikedByCurrentUser = false;
                }
            }

            var perfil = _context.Perfil.Find(1);

            if (perfil == null)
            {
                perfil = new perfilModel
                {
                    Id = 1,
                    NomeUsuario = "Usuário",
                    FotoDePerfil = Array.Empty<byte>(),
                    NomeArquivo = string.Empty,
                    ContentType = string.Empty
                };
                _context.Perfil.Add(perfil);
                _context.SaveChanges();
            }

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
