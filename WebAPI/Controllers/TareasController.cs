using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.ViewModels.Tareas;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareasService _tareasService;

        public TareasController(ITareasService tareasService)
        {
            _tareasService = tareasService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TareasViewModel>>> GetAllTareas()
        {
            var tareas = await _tareasService.GetAllAsync();
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareasViewModel>> GetTareaById(int id)
        {
            var tarea = await _tareasService.GetByIdAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTarea(SaveTareasViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tareasService.AddAsync(viewModel);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTarea(int id, SaveTareasViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest("ID en el cuerpo del mensaje no coincide con el ID de la URL");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tareasService.UpdateAsync(viewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTarea(int id)
        {
            var tarea = await _tareasService.GetByIdAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            await _tareasService.DeleteAsync(id);
            return Ok();
        }
    }
}

