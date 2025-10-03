using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View(posts);

        }
        public IActionResult Postar()
        {
            return View();
        }

        /* resolver isso 
         An unhandled exception occurred while processing the request.
    InvalidOperationException: The view 'Enviar' was not found. The following locations were searched:
    /Views/Perfil/Enviar.cshtml
    /Views/Shared/Enviar.cshtml
         */

        [HttpPost]
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

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Selecione uma imagem válida.");
            return RedirectToAction("Index");
        }

        public IActionResult Exibir(int id)
        {
            var foto = _context.Post.Find(id);
            if (foto == null) return NotFound();

            return File(foto.Imagem, foto.ContentType);
        }
    }
}
