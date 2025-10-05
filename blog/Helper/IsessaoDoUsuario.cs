using blog.Models;

namespace blog.Helper
{
    public interface IsessaoDoUsuario
    {
        
            void CriarSessaoDoUsuario(UsuarioModel login);
            void RemoverSessaoDoUsuario();
            UsuarioModel BuscarSessaoDoUsuario();
        
    }
}
