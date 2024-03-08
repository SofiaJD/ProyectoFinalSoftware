using Domain.Common;

namespace Domain.Entities
{
    public class Usuarios :  BaseEntity
    {
        public string? NombreUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? RolUsuario { get; set; }
        public string? Password { get; set; }
        // Relación muchos a muchos con Proyecto
        public List<Proyectos> Proyectos { get; set; }
    } 
}
