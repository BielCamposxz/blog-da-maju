using blog.Models;

namespace blog.repositorios.Login
{
    public interface ILogin
    {
        public UsuarioModel BuscarPorLogin(string login);
    }
}
