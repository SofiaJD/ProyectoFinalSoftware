using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Asignacion : BaseEntity
    {
        public int IdConsultor { get; set; }
        public virtual Consultor? Consultor { get; set; }
        public int ProyectoID { get; set; }
        public virtual Proyectos? Proyecto { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
