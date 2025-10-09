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
            var posts = _context.Post
     .OrderByDescending(p => p.DataDePostagem)
     .ToList();

            var usuario = _sessaoDoUsuario.BuscarSessaoDoUsuario();

            foreach (var post in posts)
            {
                if (usuario != null)
                {
                    // verifica se o usu�rio curtiu este post
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
                    NomeUsuario = "Usu�rio",
                    FotoDePerfil = Array.Empty<byte>(),
                    NomeArquivo = string.Empty,
                    ContentType = string.Empty
                };
                _context.Perfil.Add(perfil);
                _context.SaveChanges();
            }
            if (string.IsNullOrWhiteSpace(perfil.NomeUsuario))
            {
                perfil.NomeUsuario = "Usu�rio";
                _context.Update(perfil);
                _context.SaveChanges();
            }
            if (perfil.FotoDePerfil == null)
            {
                perfil.FotoDePerfil = Array.Empty<byte>();
                _context.Update(perfil);
                _context.SaveChanges();
            }

            var perfilPostModel = new PerfilPostModel
            {
                Posts = posts,
                Perfil = perfil
            };
            return View(perfilPostModel);
        }

        public IActionResult Bloqueio()
        {
            return View();
        }

        public IActionResult Exibir(int id)
        {
            var foto = _context.Post.Find(id);
            return File(foto.Imagem, foto.ContentType);
        }

        [HttpPost]
        public IActionResult ExcluirPost(int id)
        {
            // Busca o post no banco
            var post = _context.Post.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                TempData["MensagemErro"] = "Post n�o encontrado!";
                return RedirectToAction("Index");
            }

            try
            {
                _context.Post.Remove(post);
                _context.SaveChanges();
                TempData["MensagemSucesso"] = "Post exclu�do com sucesso!";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao excluir post: {ex.Message}");
                TempData["MensagemErro"] = "Ocorreu um erro ao excluir o post.";
            }

            return RedirectToAction("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
