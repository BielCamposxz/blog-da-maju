using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace blog.Controllers
{
    public class PerfilController : Controller
    {

        private readonly BancoContext _context;

        public PerfilController(BancoContext context)
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
            var post = _context.Post.Find(id);
            if (post == null)
                return RedirectToAction("Index", "Home");

            string sessionKey = $"liked_{id}";
            bool liked = HttpContext.Session.GetString(sessionKey) == "1";

            if (liked)
            {
                post.Likes = Math.Max(0, post.Likes - 1);
                HttpContext.Session.Remove(sessionKey);
            }
            else
            {
                post.Likes++;
                HttpContext.Session.SetString(sessionKey, "1");
            }

            _context.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
