using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Usuarios
{
    public class SaveUsuariosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre de Usuario")]
        [DataType(DataType.Text)]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Apellido")]
        [DataType(DataType.Text)]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Correo Electronico")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Numero de telefono")]
        [DataType(DataType.Text)]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Rol de Usuario")]
        [DataType(DataType.Text)]
        public string? RolUsuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Contrasenia")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las Contrasenias deben coincidir")]
        [Required(ErrorMessage = "Debe ingresar una Contrasenia")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
