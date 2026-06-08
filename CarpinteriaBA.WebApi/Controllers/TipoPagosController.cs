using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.TipoDePago;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagosController : ControllerBase
    {
        private readonly ILogger<TipoPagosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<TipoPago> _tipoPago;
        private readonly IMapper _mapper;

        public TipoPagosController(ILogger<TipoPagosController> logger,
            IStringService stringService,
            IApplication<TipoPago> tipoPago,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _tipoPago = tipoPago;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<TipoPagoResponseDto>>(_tipoPago.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            TipoPago tipoPago = _tipoPago.GetById(Id.Value);
            if (tipoPago is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TipoPagoResponseDto>(tipoPago));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoPagoRequestDto tipoPagoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var tipoPago = _mapper.Map<TipoPago>(tipoPagoRequestDto);
            _tipoPago.Save(tipoPago);
            return Ok(tipoPago.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, TipoPagoRequestDto tipoPagoRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            TipoPago tipoPagoBack = _tipoPago.GetById(Id.Value);

            if (tipoPagoBack is null)
            { return NotFound(); }

            _mapper.Map(tipoPagoRequestDto, tipoPagoBack);

            _tipoPago.Save(tipoPagoBack);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            TipoPago tipoPagoBack = _tipoPago.GetById(Id.Value);
            if (tipoPagoBack is null)
            { return NotFound(); }
            _tipoPago.Delete(tipoPagoBack.Id);
            return Ok();
        }
    }
}
