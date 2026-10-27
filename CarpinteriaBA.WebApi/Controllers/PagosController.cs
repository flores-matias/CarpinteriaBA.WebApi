using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.Pago;
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
    public class PagosController : ControllerBase
    {
        private readonly ILogger<PagosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Pago> _pago;
        private readonly IApplication<Pedido> _pedido;
        private readonly IMapper _mapper;

        public PagosController(ILogger<PagosController> logger,
            IStringService stringService,
            IApplication<Pago> pago,
            IApplication<Pedido> pedido,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _pago = pago;
            _pedido = pedido;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<PagoResponseDto>>(_pago.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Pago pago = _pago.GetById(Id.Value);
            if (pago is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PagoResponseDto>(pago));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PagoRequestDto pagoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }

            Pedido pedido = _pedido.GetById(pagoRequestDto.IdPedido);
            if (pedido is null)
            { return BadRequest("El pedido no existe."); }

            // Validaciones del negocio (el monto disponible es todo el saldo del pedido)
            string? error = ValidarPago(pedido, pagoRequestDto, pedido.Saldo);
            if (error is not null)
            { return BadRequest(error); }

            var pago = _mapper.Map<Pago>(pagoRequestDto);
            _pago.Save(pago);

            // El pago nuevo ya cuenta en pedido.Pagos: se recalcula Pendiente / Parcial / Pagado
            pedido.ActualizarEstadoPago();
            _pedido.Save(pedido);

            return Ok(pago.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, PagoRequestDto pagoRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            Pago pagoBack = _pago.GetById(Id.Value);

            if (pagoBack is null)
            { return NotFound(); }

            // Un pago no se mueve de pedido: si se cargo en el pedido equivocado, se borra y se carga de nuevo
            if (pagoRequestDto.IdPedido != pagoBack.IdPedido)
            { return BadRequest("No se puede cambiar el pedido de un pago. Eliminalo y cargalo en el pedido correcto."); }

            Pedido pedido = _pedido.GetById(pagoBack.IdPedido);

            // Al editar, el monto anterior de este pago "vuelve" al saldo disponible
            decimal disponible = pedido.Saldo + pagoBack.Monto;
            string? error = ValidarPago(pedido, pagoRequestDto, disponible);
            if (error is not null)
            { return BadRequest(error); }

            _mapper.Map(pagoRequestDto, pagoBack);
            _pago.Save(pagoBack);

            pedido.ActualizarEstadoPago();
            _pedido.Save(pedido);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            Pago pagoBack = _pago.GetById(Id.Value);
            if (pagoBack is null)
            { return NotFound(); }

            int idPedido = pagoBack.IdPedido;
            _pago.Delete(pagoBack.Id);

            // Sin este pago, el pedido puede volver a Parcial o Pendiente
            Pedido pedido = _pedido.GetById(idPedido);
            if (pedido is not null)
            {
                pedido.ActualizarEstadoPago();
                _pedido.Save(pedido);
            }

            return Ok();
        }

        /// <summary>
        /// Reglas comunes de alta y edicion. Devuelve el mensaje de error, o null si el pago es valido.
        /// </summary>
        private static string? ValidarPago(Pedido pedido, PagoRequestDto pago, decimal disponible)
        {
            if (pedido.EstadoPedido == EstadosPedido.Cancelado)
            {
                return "No se pueden registrar pagos en un pedido cancelado.";
            }
            if (pago.FechaPago.Date > DateTime.Today)
            {
                return "La fecha de pago no puede ser posterior a hoy.";
            }
            if (disponible <= 0)
            {
                return "El pedido no tiene saldo pendiente.";
            }
            if (pago.Monto > disponible)
            {
                return $"El monto supera el saldo del pedido (${disponible:N2}).";
            }
            return null;
        }
    }
}