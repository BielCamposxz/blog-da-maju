using blog.Enum;
using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class LoginModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "Digite um login válido")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite uma senha válida")]
        public string Senha { get; set; }

    }
}
