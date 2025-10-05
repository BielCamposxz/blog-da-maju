using blog.Helper;
using blog.Models;
using blog.repositorios.Login;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogin _login;
        private readonly IsessaoDoUsuario _sessaoDoUsuario;

        public LoginController(ILogin login, IsessaoDoUsuario sessaoDoUsuario)
        {
            _login = login;
            _sessaoDoUsuario = sessaoDoUsuario;
        }

        public IActionResult Index()
        {
            if (_sessaoDoUsuario.BuscarSessaoDoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Sair()
        {
            _sessaoDoUsuario.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult Entrar(LoginModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel login = _login.BuscarPorLogin(usuario.Login);

                    if (login != null)
                    {
                        if (login.ValidarSenha(usuario.Senha))
                        {
                            _sessaoDoUsuario.CriarSessaoDoUsuario(login);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Senha inválida";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Usuário e/ou senha inválidos";
                    }

                    return View("Index");
                }

                TempData["MensagemErro"] = "Preencha os dados corretamente";
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo deu errado";
                return RedirectToAction("Index");
            }
        }
    }
}
