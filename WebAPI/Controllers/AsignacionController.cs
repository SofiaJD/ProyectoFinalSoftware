using Application.Interfaces.Services;
using Application.ViewModels.Asignacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsignacionController : ControllerBase
    {
        private readonly IAsignacionService _asignacionService;

        public AsignacionController(IAsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AsignarConsultorAProyecto(SaveAsignacionViewModel viewModel)
        {
            try
            {
                await _asignacionService.AsignarConsultorAProyectoAsync(viewModel);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActualizarAsignacion(int id, SaveAsignacionViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest("El ID en el cuerpo del mensaje no coincide con el ID de la URL");
            }

            try
            {
                await _asignacionService.ActualizarAsignacionAsync(viewModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{consultorId}/{proyectoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EliminarAsignacion(int consultorId, int proyectoId)
        {
            try
            {
                await _asignacionService.EliminarAsignacionAsync(consultorId, proyectoId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("consultor/{consultorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AsignacionViewModel>>> ListarAsignacionesPorConsultor(int consultorId)
        {
            try
            {
                var asignaciones = await _asignacionService.ListarAsignacionesPorConsultorAsync(consultorId);
                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("proyecto/{proyectoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AsignacionViewModel>>> ListarAsignacionesPorProyecto(int proyectoId)
        {
            try
            {
                var asignaciones = await _asignacionService.ListarAsignacionesPorProyectoAsync(proyectoId);
                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{consultorId}/{proyectoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AsignacionViewModel>> GetById(int consultorId, int proyectoId)
        {
            try
            {
                var asignacion = await _asignacionService.GetByIdAsync(consultorId, proyectoId);
                if (asignacion == null)
                {
                    return NotFound();
                }
                return Ok(asignacion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
