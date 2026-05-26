using CarpinteriaBA.Application;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController:ControllerBase
    {
        private readonly ILogger<Cliente> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Cliente> _cliente;

        public ClientesController(ILogger<Cliente> logger, IStringService stringService, IApplication<Cliente> cliente)
        {
            _logger = logger;
            _stringService = stringService;
            _cliente = cliente;
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_cliente.GetAll());
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Cliente cliente = _cliente.GetById(Id.Value);
            if (cliente is null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            _cliente.Save(cliente);
            return Ok(cliente.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, Cliente cliente)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Cliente clienteBack = _cliente.GetById(Id.Value);
            if (clienteBack is null)
            { return NotFound(); }
            clienteBack.Nombre = cliente.Nombre;
            clienteBack.Apellido = cliente.Apellido;
            clienteBack.Telefono = cliente.Telefono;
            clienteBack.Direccion = cliente.Direccion;
            clienteBack.Email = cliente.Email;
            _cliente.Save(clienteBack);
            return Ok(clienteBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Cliente clienteBack = _cliente.GetById(Id.Value);
            if (clienteBack is null)
            { return NotFound(); }
            _cliente.Delete(clienteBack.Id);
            return Ok();
        }
    }
}
