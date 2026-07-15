using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.Pago;
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
    public class PagosController : ControllerBase
    {
        private readonly ILogger<PagosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Pago> _pago;
        private readonly IMapper _mapper;

        public PagosController(ILogger<PagosController> logger,
            IStringService stringService,
            IApplication<Pago> pago,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _pago = pago;
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
            var pago = _mapper.Map<Pago>(pagoRequestDto);
            _pago.Save(pago);
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

            _mapper.Map(pagoRequestDto, pagoBack);

            _pago.Save(pagoBack);

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
            _pago.Delete(pagoBack.Id);
            return Ok();
        }
    }
}
