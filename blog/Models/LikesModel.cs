namespace blog.Models
{
    public class LikesModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UsuarioId { get; set; } // id do usuário que curtiu
        public DateTime DataDeLike { get; set; }

        public PostModel Post { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
