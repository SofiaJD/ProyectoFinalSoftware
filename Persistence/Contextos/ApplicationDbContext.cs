using System.Reflection;
using System.Threading;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contextos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Creado = DateTime.Now;
                        entry.Entity.CreadoPor = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UltimaModificacion = DateTime.Now;
                        entry.Entity.UltimaModificacionPor = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #region "Tablas"
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Clientes>().ToTable("Clientes");
            modelBuilder.Entity<Consultor>().ToTable("Consultores");
            modelBuilder.Entity<Asignacion>().ToTable("Asignaciones");
            modelBuilder.Entity<Tareas>().ToTable("Tareas");
            modelBuilder.Entity<Proyectos>().ToTable("Proyectos");
            #endregion

            #region "Primary keys"
            modelBuilder.Entity<Usuarios>().HasKey(u => u.Id);
            modelBuilder.Entity<Proyectos>().HasKey(p => p.Id);
            modelBuilder.Entity<Clientes>().HasKey(c => c.Id);
            modelBuilder.Entity<Tareas>().HasKey(t => t.Id);
            modelBuilder.Entity<Consultor>().HasKey(c => c.Id);
            modelBuilder.Entity<Asignacion>().HasKey(a => new { a.IdConsultor, a.ProyectoID });
            modelBuilder.Entity<Asignacion>().HasKey(a => a.Id);

            #endregion

            #region "Relaciones"
            // Relación entre Usuario y Proyecto (muchos a muchos)
            modelBuilder.Entity<Usuarios>()
                .HasMany(u => u.Proyectos)
                .WithMany(p => p.usuarios);

            // Relación entre Proyecto y Tarea (uno a muchos)
            modelBuilder.Entity<Tareas>()
                .HasOne(t => t.Proyecto)
                .WithMany(p => p.Tareas)
                .HasForeignKey(t => t.ProyectoID);

            // Relación entre Proyecto y Cliente (muchos a uno)
            modelBuilder.Entity<Proyectos>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.proyectos)
                .HasForeignKey(p => p.ClienteID);

            // Relación entre Consultor y Proyecto (muchos a muchos)
            modelBuilder.Entity<Asignacion>()
                .HasKey(a => new { a.IdConsultor, a.ProyectoID });

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Consultor)
                .WithMany(c => c.Asignaciones)
                .HasForeignKey(a => a.IdConsultor);

            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Proyecto)
                .WithMany(p => p.Asignaciones)
                .HasForeignKey(a => a.ProyectoID);
            #endregion
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Consultor> Consultores { get; set; }
        public DbSet<Asignacion> Asignacion { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }


    }
}
