using blog.Helper;
using blog.Models;
using blog.repositorios.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class CadastroController : Controller
 {
        private readonly ICadrastro _cadrastro;
        private readonly IsessaoDoUsuario _sessaoDoUsuario;


        public CadastroController(ICadrastro cadrastro, IsessaoDoUsuario sessaoDoUsuario)
        {
            _cadrastro = cadrastro;
            _sessaoDoUsuario = sessaoDoUsuario;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuario)
        {
            if(ModelState.IsValid)
            {
                if(_cadrastro.UsuarioExiste(usuario) != null)
                {
                    TempData["MensagemErro"] = _cadrastro.UsuarioExiste(usuario);
                    return View("Index");
                }
                _sessaoDoUsuario.CriarSessaoDoUsuario(usuario);
               _cadrastro.Cadrastar(usuario);
               return RedirectToAction("Index", "Home");

            }
            return View("Index");
        }
    }
}
