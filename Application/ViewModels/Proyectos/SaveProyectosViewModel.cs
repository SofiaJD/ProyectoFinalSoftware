using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Proyectos
{
    public class SaveProyectosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre de proyecto")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripcion de proyecto")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha de inicio")]
        [DataType(DataType.Text)]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Fecha para finalizar")]
        [DataType(DataType.Text)]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "Debe ingresar un estado para el proyecto")]
        [DataType(DataType.Text)]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Cliente")]
        [DataType(DataType.Text)]
        public int ClienteID { get; set; }
    }
}
