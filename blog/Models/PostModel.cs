using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public DateTime DataDePostagem { get; set; }

        public int Likes { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória")]
        [MinLength(1, ErrorMessage = "A imagem não pode estar vazia")]
        public byte[] Imagem { get; set; } = Array.Empty<byte>();

        public string NomeArquivo { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Hashtags devem ter no máximo 200 caracteres")]
        public string Hashtags { get; set; } = string.Empty;

        [NotMapped]
        public bool LikedByCurrentUser { get; set; }
    }
}
