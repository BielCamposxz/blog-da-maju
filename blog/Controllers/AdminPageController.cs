using blog.Data;
using blog.filter;
using blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [PagiSomenteAdm]
    public class AdminPageController : Controller
    {

        private readonly BancoContext _context;

        public AdminPageController(BancoContext context)
        {
            _context = context;
        }

        public List<UsuarioModel> buscarUsuarios()
        {
            return _context.Usuario.ToList();
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = buscarUsuarios();
            return View(usuarios);
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _context.Usuario.FirstOrDefault(x => x.Id == id);
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarDoBanco(UsuarioModel usuarioModel)
        {
            try
            {
                UsuarioModel usuario = BuscarPorId(usuarioModel.Id);

                if (usuario != null)
                {
                    _context.Usuario.Remove(usuario);
                    _context.SaveChanges();
                    TempData["MensagemSucesso"] = "Todos os dados do usuario foram apagados!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, algo deu errado tente novamente mais tarde";
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado tente novamente mais tarde";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModel usuario)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    UsuarioModel usuarioDb = BuscarPorId(usuario.Id);

                    usuarioDb.Nome = usuario.Nome;
                    usuarioDb.Login = usuario.Login;
                    usuarioDb.Email = usuario.Email;
                    usuarioDb.Senha = usuario.Senha;
                    _context.Usuario.Update(usuarioDb);
                    _context.SaveChanges();
                    TempData["MensagemSucesso"] = "Usuario editado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return RedirectToAction("Index");
            }
        }
    }
}
