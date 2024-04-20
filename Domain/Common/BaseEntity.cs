using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }
        public string CreadoPor { get; set; } = string.Empty;
        public DateTime Creado { get; set; } = DateTime.Now;
        public DateTime UltimaModificacion { get; set; } = DateTime.Now;
        public string UltimaModificacionPor { get; set; } = string.Empty;
    }
}
