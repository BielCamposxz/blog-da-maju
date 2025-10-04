namespace blog.Models
{
    public class PerfilPostModel
    {
        public List<PostModel> Posts { get; set; } = new();
        public perfilModel Perfil { get; set; }
    }
}
