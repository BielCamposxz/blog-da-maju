using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using blog.Helper;

namespace blog.Controllers
{
    public class PerfilController : Controller
    {

        private readonly BancoContext _context;
        private readonly IsessaoDoUsuario _sessaoDoUsuario;

        public PerfilController(BancoContext context, IsessaoDoUsuario sessaoDoUsuario)
        {
            _context = context; 
            _sessaoDoUsuario = sessaoDoUsuario;
        }
        public IActionResult Index()
        {
            var posts = _context.Post.ToList();
            foreach (var post in posts)
            {
                string sessionKey = $"liked_{post.Id}";
                post.LikedByCurrentUser = HttpContext.Session.GetString(sessionKey) == "1";
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
            if (string.IsNullOrWhiteSpace(perfil.NomeUsuario))
            {
                perfil.NomeUsuario = "Usuário";
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
        public IActionResult Postar()
        {
            return View();
        }
        public IActionResult Editar()
        {
            var perfil = _context.Perfil.Find(1);
            return View(perfil);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarPerfil(perfilModel perfil, IFormFile arquivo)
        {
            var existente = await _context.Perfil.FindAsync(1);

            if (existente == null)
            {
                existente = new perfilModel { Id = 1 };
                if (arquivo != null && arquivo.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await arquivo.CopyToAsync(ms);
                    existente.NomeArquivo = arquivo.FileName;
                    existente.FotoDePerfil = ms.ToArray();
                    existente.ContentType = arquivo.ContentType;
                }
                existente.NomeUsuario = perfil.NomeUsuario;
                _context.Perfil.Add(existente);
            }
            else
            {
                if (arquivo != null && arquivo.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await arquivo.CopyToAsync(ms);
                    existente.NomeArquivo = arquivo.FileName;
                    existente.FotoDePerfil = ms.ToArray();
                    existente.ContentType = arquivo.ContentType;
                }
                existente.NomeUsuario = perfil.NomeUsuario;
                _context.Perfil.Update(existente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Perfil");
        }

       

        public IActionResult ExibirFotoPerfil(int id)
        {
            var perfil = _context.Perfil.Find(id);
            if (perfil == null || perfil.FotoDePerfil == null || perfil.FotoDePerfil.Length == 0)
                return NotFound();

            return File(perfil.FotoDePerfil, perfil.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(PostModel post, IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {   
                using var ms = new MemoryStream();
                await arquivo.CopyToAsync(ms);

                post.NomeArquivo = arquivo.FileName;
                post.Imagem = ms.ToArray();
                post.ContentType = arquivo.ContentType;
                post.Likes = 0;
                post.DataDePostagem = DateTime.Now;

                _context.Post.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Selecione uma imagem válida.");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Exibir(int id)
        {
            var foto = _context.Post.Find(id);
            if (foto == null) return NotFound();

            return File(foto.Imagem, foto.ContentType);
        }

        [HttpPost]
        public IActionResult ToggleLike(int id)
        {
            var usuario = _sessaoDoUsuario.BuscarSessaoDoUsuario();
            if (usuario == null) return RedirectToAction("Index", "Login");

            var post = _context.Post.Find(id);
            if (post == null) return RedirectToAction("Index", "Home");

            // Verifica se o usuário já curtiu
            var likeExistente = _context.LikesModel.FirstOrDefault(l => l.PostId == id && l.UsuarioId == usuario.Id);

            if (likeExistente != null)
            {
                // Se já curtiu, remove o like
                _context.LikesModel.Remove(likeExistente);
                post.Likes = Math.Max(0, post.Likes - 1);
            }
            else
            {
                // Se não curtiu ainda, adiciona like
                _context.LikesModel.Add(new LikesModel
                {
                    PostId = id,
                    UsuarioId = usuario.Id,
                    DataDeLike = DateTime.Now
                });
                post.Likes++;
            }

            _context.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
