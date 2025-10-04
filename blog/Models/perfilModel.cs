namespace blog.Models
{
    public class perfilModel
    {
        public int Id { get; set; }
        public string ContentType { get; set; } = string.Empty;

        public string NomeArquivo { get; set; } = string.Empty;
        public byte[] FotoDePerfil { get; set; } = Array.Empty<byte>();
        public string NomeUsuario { get; set; } = string.Empty;
    }
}
