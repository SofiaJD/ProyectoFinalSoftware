using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contextos;

namespace Persistence.Repositorios
{
    public class ProyectosRepository : GenericRepository<Proyectos>, IProyectosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProyectosRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetProyectoNombreByTareaIdAsync(int tareaId)
        {
            var proyectoId = await _dbContext.Tareas
                .Where(t => t.Id == tareaId)
                .Select(t => t.ProyectoID)
                .FirstOrDefaultAsync();

            if (proyectoId == default(int))
            {
                throw new ArgumentException("No se encontró la tarea con el ID proporcionado.");
            }

            var proyectoNombre = await _dbContext.Proyectos
                .Where(p => p.Id == proyectoId)
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            return proyectoNombre;
        }

        public async Task<Dictionary<int, string>> GetProyectoNombresByIDsAsync(List<int> proyectoIds)
        {
            var proyectoNombres = await _dbContext.Proyectos
                .Where(p => proyectoIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, p => p.Nombre);

            return proyectoNombres;
        }

        public async Task<string> GetProyectoNombreByAsignacionIdAsync(int asignacionId)
        {
            var proyectoId = await _dbContext.Asignacion
                .Where(a => a.Id == asignacionId)
                .Select(a => a.ProyectoID)
                .FirstOrDefaultAsync();

            if (proyectoId == default(int))
            {
                throw new ArgumentException("No se encontró la asignacion con el ID del proyecto proporcionado.");
            }

            var proyectoNombre = await _dbContext.Proyectos
                .Where(p => p.Id == proyectoId)
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            return proyectoNombre;
        }

    }
}

