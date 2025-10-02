namespace blog.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public DateTime DataDePostagem { get; set; }

        public int Likes { get; set; }

        public string Descricao { get; set; }

        public byte[] Imagem { get; set; } = Array.Empty<byte>();

        public string NomeArquivo { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

    }
}
