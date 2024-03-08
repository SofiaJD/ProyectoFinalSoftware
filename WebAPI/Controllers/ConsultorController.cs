using Application.Interfaces.Services;
using Application.Services;
using Application.ViewModels.Consultor;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorController : Controller
    {
        private readonly IConsultorService _consultorService;

        public ConsultorController(IConsultorService consultorService)
        {
            _consultorService = consultorService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddConsultor([FromBody] SaveConsultorViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //if (await _consultorService.IsEmailRegisteredAsync(vm.Email))
                //{
                //    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                //    return BadRequest(ModelState);
                //}

                //if (await _consultorService.IsTelefonoRegisteredAsync(vm.Telefono))
                //{
                //    ModelState.AddModelError("Telefono", "El teléfono ya está registrado.");
                //    return BadRequest(ModelState);
                //}

                await _consultorService.AddAsync(vm);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteConsultor(int id)
        {
            try
            {
                await _consultorService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ConsultorViewModel>>> GetAllConsultores()
        {
            try
            {
                var consultores = await _consultorService.GetAllAsync();
                return Ok(consultores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConsultorViewModel>> GetConsultorById(int id)
        {
            try
            {
                var consultor = await _consultorService.GetByIdAsync(id);
                if (consultor == null)
                {
                    return NotFound();
                }
                return Ok(consultor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateConsultor([FromBody] SaveConsultorViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _consultorService.IsEmailRegisteredForOtherConsultorAsync(vm.Id, vm.Email))
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado para otro consultor.");
                    return BadRequest(ModelState);
                }

                if (await _consultorService.IsTelefonoRegisteredForOtherConsultorAsync(vm.Id, vm.Telefono))
                {
                    ModelState.AddModelError("Telefono", "El teléfono ya está registrado para otro consultor.");
                    return BadRequest(ModelState);
                }

                await _consultorService.UpdateAsync(vm);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}


