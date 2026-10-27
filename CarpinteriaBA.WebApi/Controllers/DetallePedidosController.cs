using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.DetallePedido;
using CarpinteriaBA.Entities;
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
    public class DetallePedidosController : ControllerBase
    {
        private readonly ILogger<DetallePedidosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<DetallePedido> _detallePedido;
        private readonly IApplication<Pedido> _pedido;
        private readonly IMapper _mapper;

        public DetallePedidosController(ILogger<DetallePedidosController> logger,
            IStringService stringService,
            IApplication<DetallePedido> detallePedido,
            IApplication<Pedido> pedido,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _detallePedido = detallePedido;
            _pedido = pedido;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<DetallePedidoResponseDto>>(_detallePedido.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            DetallePedido detallePedido = _detallePedido.GetById(id.Value);
            if (detallePedido is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DetallePedidoResponseDto>(detallePedido));
        }

        /// <summary>GET api/DetallePedidos/ByPedido?IdPedido=... Solo los detalles de un pedido.</summary>
        [HttpGet]
        [Route("ByPedido")]
        public async Task<IActionResult> ByPedido(int? idPedido)
        {
            if (!idPedido.HasValue)
            {
                return BadRequest();
            }
            Pedido pedido = _pedido.GetById(idPedido.Value);
            if (pedido is null)
            {
                return NotFound();
            }
            // Con lazy loading, EF trae solo los detalles de este pedido
            return Ok(_mapper.Map<IList<DetallePedidoResponseDto>>(pedido.DetallesPedidos.ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DetallePedidoRequestDto detallePedidoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }

            Pedido pedido = _pedido.GetById(detallePedidoRequestDto.IdPedido);
            if (pedido is null)
            { return BadRequest("El pedido no existe."); }

            if (pedido.EstaCerrado)
            { return BadRequest($"No se pueden modificar los muebles de un pedido {pedido.EstadoPedido.ToLower()}."); }

            if (pedido.DetallesPedidos.Any(d => d.IdMueble == detallePedidoRequestDto.IdMueble))
            { return BadRequest("Ese mueble ya esta en el pedido. Edita la cantidad en lugar de agregarlo de nuevo."); }

            // Nuevo total = lo que ya hay + el subtotal de este detalle
            decimal nuevoTotal = pedido.TotalDetalles
                + detallePedidoRequestDto.Cantidad * detallePedidoRequestDto.PrecioVentaReal;
            try
            {
                pedido.ActualizarTotalAPagar(nuevoTotal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            var detallePedido = _mapper.Map<DetallePedido>(detallePedidoRequestDto);
            _detallePedido.Save(detallePedido);
            _pedido.Save(pedido);

            return Ok(detallePedido.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? id, DetallePedidoRequestDto detallePedidoRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            DetallePedido detallePedidoBack = _detallePedido.GetById(id.Value);

            if (detallePedidoBack is null)
            { return NotFound(); }

            // Un detalle no se mueve de pedido
            if (detallePedidoRequestDto.IdPedido != detallePedidoBack.IdPedido)
            { return BadRequest("No se puede cambiar el pedido de un detalle."); }

            Pedido pedido = _pedido.GetById(detallePedidoBack.IdPedido);

            if (pedido.EstaCerrado)
            { return BadRequest($"No se pueden modificar los muebles de un pedido {pedido.EstadoPedido.ToLower()}."); }

            // Si cambio el mueble, que no sea uno que ya esta en otro renglon del pedido
            if (pedido.DetallesPedidos.Any(d => d.IdMueble == detallePedidoRequestDto.IdMueble
                                              && d.Id != detallePedidoBack.Id))
            { return BadRequest("Ese mueble ya esta en el pedido."); }

            // Nuevo total = lo que hay - subtotal viejo + subtotal nuevo
            decimal nuevoTotal = pedido.TotalDetalles
                - detallePedidoBack.Subtotal
                + detallePedidoRequestDto.Cantidad * detallePedidoRequestDto.PrecioVentaReal;
            try
            {
                pedido.ActualizarTotalAPagar(nuevoTotal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            _mapper.Map(detallePedidoRequestDto, detallePedidoBack);
            _detallePedido.Save(detallePedidoBack);
            _pedido.Save(pedido);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            DetallePedido detallePedidoBack = _detallePedido.GetById(id.Value);
            if (detallePedidoBack is null)
            { return NotFound(); }

            Pedido pedido = _pedido.GetById(detallePedidoBack.IdPedido);

            if (pedido.EstaCerrado)
            { return BadRequest($"No se pueden modificar los muebles de un pedido {pedido.EstadoPedido.ToLower()}."); }

            // Nuevo total = lo que hay - el subtotal del detalle que se quita
            decimal nuevoTotal = pedido.TotalDetalles - detallePedidoBack.Subtotal;
            try
            {
                pedido.ActualizarTotalAPagar(nuevoTotal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            _detallePedido.Delete(detallePedidoBack.Id);
            _pedido.Save(pedido);

            return Ok();
        }
    }
}