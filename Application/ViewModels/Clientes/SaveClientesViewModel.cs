using System.ComponentModel.DataAnnotations;


namespace Application.ViewModels.Clientes
{
    public class SaveClientesViewModel
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Debe ingresar un Nombre de cliente")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar una informacion de contacto para el cliente")]
        [DataType(DataType.Text)]
        public string? Contacto { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Email de para el cliente")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Telefono para el cliente")]
        [DataType(DataType.Text)]
        public string? Telefono { get; set; }
    }
}
