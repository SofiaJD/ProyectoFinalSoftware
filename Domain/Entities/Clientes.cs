using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Clientes : BaseEntity
    {
        public string? Nombre { get; set; }
        public string? Contacto { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        public List<Proyectos> proyectos { get; set; }
    }
}
