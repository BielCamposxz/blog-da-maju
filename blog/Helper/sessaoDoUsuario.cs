using blog.Models;
using Newtonsoft.Json;

namespace blog.Helper
{
    public class sessaoDoUsuario : IsessaoDoUsuario
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public sessaoDoUsuario(IHttpContextAccessor httpContextAccessor)
        {
                _httpContextAccessor = httpContextAccessor;
        }

        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContextAccessor.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            }
        }

        public void CriarSessaoDoUsuario(UsuarioModel login)
        {
            string usuario = JsonConvert.SerializeObject(login);
            _httpContextAccessor.HttpContext.Session.SetString("SessaoUsuarioLogado", usuario);
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpContextAccessor.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}
