using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.Pedido;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Enums;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        /// <summary>Lista de estados validos, para que el front arme el select sin repetirlos.</summary>
        [HttpGet]
        [Route("Estados")]
        public IActionResult Estados()
        {
            return Ok(EstadosPedido.Todos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidoRequestDto pedidoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }

            if (!EstadosPedido.EsValido(pedidoRequestDto.EstadoPedido))
            { return BadRequest("El estado del pedido no es valido."); }

            var pedido = _mapper.Map<Pedido>(pedidoRequestDto);

            // Datos que pone la API, no el usuario
            pedido.FechaCreacion = DateTime.Now;
            pedido.TotalAPagar = 0;                       // se calcula al cargar los detalles
            pedido.EstadoPago = EstadosPago.Pendiente;    // todavia no hay pagos

            if (pedido.FechaEntregaPrometida.Date < pedido.FechaCreacion.Date)
            { return BadRequest("La fecha de entrega no puede ser anterior a hoy."); }

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

            if (!EstadosPedido.EsValido(pedidoRequestDto.EstadoPedido))
            { return BadRequest("El estado del pedido no es valido."); }

            Pedido pedidoBack = _pedido.GetById(Id.Value);

            if (pedidoBack is null)
            { return NotFound(); }

            if (pedidoRequestDto.FechaEntregaPrometida.Date < pedidoBack.FechaCreacion.Date)
            { return BadRequest("La fecha de entrega no puede ser anterior a la fecha de creacion del pedido."); }

            // Solo cambia cliente, fecha de entrega y estado. Total y estado de pago no se tocan.
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