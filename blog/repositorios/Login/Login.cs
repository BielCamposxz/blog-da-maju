using blog.Data;
using blog.Models;

namespace blog.repositorios.Login
{
    public  class Login : ILogin
    {
        private readonly BancoContext _bancoContext;

        public Login(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel  BuscarPorLogin(string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(user => user.Login.ToUpper() == login.ToUpper());
        }
    }
}
