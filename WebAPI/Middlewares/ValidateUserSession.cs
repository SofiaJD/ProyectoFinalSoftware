using Application.ViewModels.Usuarios;
using Application.Helpers;

namespace WebAPI.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            UsuariosViewModel usuarioViewModel = _contextAccessor.HttpContext.Session.Get<UsuariosViewModel>("Usuario");
            if (usuarioViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
