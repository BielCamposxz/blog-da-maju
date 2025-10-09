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

        public string UsuarioExiste(UsuarioModel usuario)
        {
            if(_context.Usuario.FirstOrDefault(user => user.Email == usuario.Email) != null)
            {
                return "Email já cadastrado, tente outro";
            }

            if((_context.Usuario.FirstOrDefault(user => user.Login == usuario.Login) != null) != null)
            {
                return "Login já cadastrado, tente outro";
            }
            return null;
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
