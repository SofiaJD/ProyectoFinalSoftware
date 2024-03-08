using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Asignacion
{
    public class SaveAsignacionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un ID de Consultor")]
        [DataType(DataType.Text)]
        public int ConsultorID { get; set; }

        [Required(ErrorMessage = "Debe ingresar un ID de proyecto")]
        [DataType(DataType.Text)]
        public int ProyectoID { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha de asignacion")]
        [DataType(DataType.DateTime)]
        public DateTime FechaAsignacion { get; set; }
    }
}
