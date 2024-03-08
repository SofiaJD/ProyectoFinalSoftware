using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Consultor : Common.BaseEntity
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Especialidad { get; set; }
        public virtual List<Asignacion>? Asignaciones { get ; set; } 
    }
}
