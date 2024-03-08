using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Proyectos : BaseEntity
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }
        public int ClienteID { get; set; }
        public Clientes? Cliente { get; set; }  
        public List<Tareas>? Tareas { get; set; }
        public virtual List<Asignacion>? Asignaciones { get; set; }
        public List<Usuarios> usuarios { get; set; }

    }
}
