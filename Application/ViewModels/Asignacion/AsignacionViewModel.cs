using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Asignacion
{
    public class AsignacionViewModel
    {
        public int Id { get; set; }
        public string? ConsultorNombre { get; set; }
        public string? ProyectoNombre { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
