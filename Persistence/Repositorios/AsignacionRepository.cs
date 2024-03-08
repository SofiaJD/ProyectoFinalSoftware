using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.ViewModels.Asignacion;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contextos;

namespace Persistence.Repositorios
{
    public class AsignacionRepository : IAsignacionRepository 
    {
        private readonly ApplicationDbContext _dbContext;

        public AsignacionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ActualizarAsignacionAsync(Asignacion asignacion)
        {
            _dbContext.Entry(asignacion).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AsignarConsultorAProyectoAsync(Asignacion asignacion)
        {
            await _dbContext.Asignacion.AddAsync(asignacion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EliminarAsignacionAsync(Asignacion asignacion)
        {
            _dbContext.Asignacion.Remove(asignacion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Asignacion>> ListarAsignacionesPorConsultorAsync(int consultorId)
        {
           return await _dbContext.Asignacion
             .Where(a => a.ConsultorID == consultorId)
             .ToListAsync();
        }

        public async Task<List<Asignacion>> ListarAsignacionesPorProyectoAsync(int proyectoId)
        {
            return await _dbContext.Asignacion
             .Where(a => a.ProyectoID == proyectoId)
             .ToListAsync();
        }
        public async Task<Asignacion> GetByIdAsync(int id)
        {
            return await _dbContext.Asignacion.FindAsync(id);
        }
    }
}
