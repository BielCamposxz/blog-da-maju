using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class perfilModel
    {
        public int Id { get; set; }

        public string ContentType { get; set; } = string.Empty;

        public string NomeArquivo { get; set; } = string.Empty;

        public byte[] FotoDePerfil { get; set; } = Array.Empty<byte>();

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do usuário deve ter entre 3 e 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9_.]+$",
            ErrorMessage = "O nome do usuário só pode conter letras, números, underline ou ponto")]
        public string NomeUsuario { get; set; } = string.Empty;
    }
}
