using Application.Interfaces.Services;
using Application.Services;
using Application.ViewModels.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _clientesServices;

        public ClientesController(IClientesService clientesServices)
        {
            _clientesServices = clientesServices;
        }

        [HttpPost("Crear Clientes")]
        public async Task<IActionResult> CreateCliente([FromBody] SaveClientesViewModel vm)
        {
            try
            {
                await _clientesServices.AddAsync(vm);
                return Ok("Cliente creado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear cliente: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientesViewModel>>> GetAllClientes()
        {
            var clientes = await _clientesServices.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesViewModel>> GetClienteById(int id)
        {
            var cliente = await _clientesServices.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCliente(int id, SaveClientesViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }
            await _clientesServices.UpdateAsync(viewModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var cliente = await _clientesServices.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _clientesServices.DeleteAsync(id);
            return Ok();
        }
    }
}

