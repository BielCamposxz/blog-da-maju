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
            return View();
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
        public async Task<IActionResult> Enviar(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                using var ms = new MemoryStream();
                await arquivo.CopyToAsync(ms);

                var foto = new PostModel
                {
                    NomeArquivo = arquivo.FileName,
                    Imagem = ms.ToArray(),
                    ContentType = arquivo.ContentType
                };

                _context.Add(foto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Selecione uma imagem válida.");
            return View();
        }

        public IActionResult Exibir(int id)
        {
            var foto = _context.Post.Find(id);
            if (foto == null) return NotFound();

            return File(foto.Imagem, foto.ContentType);
        }
    }
}
