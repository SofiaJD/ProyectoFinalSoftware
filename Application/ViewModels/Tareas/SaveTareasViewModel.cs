using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Tareas
{
    public class SaveTareasViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre para la tarea")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripcion para la tarea")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha de inicio")]
        [DataType(DataType.Text)]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha para finalizar")]
        [DataType(DataType.Text)]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "Debe ingresar un estado para la tarea")]
        [DataType(DataType.Text)]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "Debe ingresar un ID de proyecto")]
        [DataType(DataType.Text)]
        public int ProyectoID { get; set; }
    }
}
