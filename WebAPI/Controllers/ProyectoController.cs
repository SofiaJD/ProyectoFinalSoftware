using Application.Interfaces.Services;
using Application.ViewModels.Proyectos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : Controller
    {
        private readonly IProyectosService _proyectosService;

        public ProyectoController(IProyectosService proyectosService)
        {
            _proyectosService = proyectosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProyectosViewModel>>> GetAllProyectos()
        {
            var proyectos = await _proyectosService.GetAllAsync();
            return Ok(proyectos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectosViewModel>> GetProyectoById(int id)
        {
            var proyecto = await _proyectosService.GetByIdAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            return Ok(proyecto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProyecto(SaveProyectosViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("ViewModel cannot be null");
            }

            await _proyectosService.AddAsync(viewModel);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProyecto(int id, SaveProyectosViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }
            await _proyectosService.UpdateAsync(viewModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProyecto(int id)
        {
            var proyecto = await _proyectosService.GetByIdAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            await _proyectosService.DeleteAsync(id);
            return Ok();
        }
    }
}
