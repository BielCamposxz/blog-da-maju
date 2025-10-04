using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<PostModel> Post { get; set; }

        public DbSet<perfilModel> Perfil
        {
            get; set;
        }
    }
}
