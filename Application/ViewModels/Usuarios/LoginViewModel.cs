using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Usuarios
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un Correo Electronico")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Contrasenia")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
