using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Usuarios
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? RolUsuario { get; set; }
        public string? Password { get; set; }
    }
}
