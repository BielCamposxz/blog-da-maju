using blog.Data;
using blog.Models;

namespace blog.repositorios.Cadastro
{
    public class Cadastra : ICadrastro
    {
		private readonly BancoContext _context;

        public Cadastra(BancoContext context)
        {
           _context = context;
        }

        public UsuarioModel Cadrastar(UsuarioModel usuario)
        {
			try
			{
                usuario.isAdmin = false;
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return usuario;
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
