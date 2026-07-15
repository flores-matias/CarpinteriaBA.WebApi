using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.Pedido;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Pedido> _pedido;
        private readonly IMapper _mapper;

        public PedidosController(ILogger<PedidosController> logger,
            IStringService stringService,
            IApplication<Pedido> pedido,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _pedido = pedido;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<PedidoResponseDto>>(_pedido.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Pedido pedido = _pedido.GetById(Id.Value);
            if (pedido is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PedidoResponseDto>(pedido));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidoRequestDto pedidoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var pedido = _mapper.Map<Pedido>(pedidoRequestDto);
            _pedido.Save(pedido);
            return Ok(pedido.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, PedidoRequestDto pedidoRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            Pedido pedidoBack = _pedido.GetById(Id.Value);

            if (pedidoBack is null)
            { return NotFound(); }

            _mapper.Map(pedidoRequestDto, pedidoBack);

            _pedido.Save(pedidoBack);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            Pedido pedidoBack = _pedido.GetById(Id.Value);
            if (pedidoBack is null)
            { return NotFound(); }
            _pedido.Delete(pedidoBack.Id);
            return Ok();
        }
    }
}
