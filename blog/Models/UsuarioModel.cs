using blog.Enum;
using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class UsuarioModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "digite um valor valido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "digite um valor valido")]
        public string Login { get; set; }

        [Required(ErrorMessage = "digite um valor valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "digite um valor valido")]
        public string Senha { get; set; }

        public PerfilEnum Perfil { get; set; }

        public bool isAdmin { get; set; }

        public bool ValidarSenha(string senha)
        {
            return Senha == senha;
        }
    }
}
