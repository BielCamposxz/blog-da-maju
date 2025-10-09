using blog.Models;

namespace blog.repositorios.Cadastro
{
    public interface ICadrastro
    {
        public string UsuarioExiste(UsuarioModel usuario);
        public UsuarioModel Cadrastar(UsuarioModel usuario);
    }
}
