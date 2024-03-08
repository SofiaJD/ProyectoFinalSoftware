using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Consultor
{
    public class SaveConsultorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre de consultor")]
        [DataType(DataType.Text)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Email de para el consultor")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Telefono para el consultor")]
        [DataType(DataType.Text)]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El consultor debe tener una especialidad")]
        [DataType(DataType.Text)]
        public string? Especialidad { get; set; }

        public List<int>? IdsAsignaciones { get; set; }
    }
}
