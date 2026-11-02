using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.MuebleConfiguracion;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Enums;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    /// <summary>
    /// Recargos opcionales de cada mueble (IVA, Servicio personalizado, etc.).
    /// "Mano de obra" y "Uso de maquinas" NO van aca: se aplican siempre a todos los muebles.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MuebleConfiguracionesController : ControllerBase
    {
        private readonly ILogger<MuebleConfiguracionesController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<MuebleConfiguracion> _muebleConfiguracion;
        private readonly IApplication<Mueble> _mueble;
        private readonly IApplication<Configuracion> _configuracion;
        private readonly IMapper _mapper;

        public MuebleConfiguracionesController(ILogger<MuebleConfiguracionesController> logger,
            IStringService stringService,
            IApplication<MuebleConfiguracion> muebleConfiguracion,
            IApplication<Mueble> mueble,
            IApplication<Configuracion> configuracion,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _muebleConfiguracion = muebleConfiguracion;
            _mueble = mueble;
            _configuracion = configuracion;
            _mapper = mapper;
        }

        /// <summary>GET api/MuebleConfiguraciones/ByMueble?IdMueble=... Recargos de un mueble.</summary>
        [HttpGet]
        [Route("ByMueble")]
        public async Task<IActionResult> ByMueble(int? IdMueble)
        {
            if (!IdMueble.HasValue)
            {
                return BadRequest();
            }
            Mueble mueble = _mueble.GetById(IdMueble.Value);
            if (mueble is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IList<MuebleConfiguracionResponseDto>>(
                mueble.ConfiguracionesExtra.OrderBy(e => e.Id).ToList()));
        }

        /// <summary>POST api/MuebleConfiguraciones: agrega un recargo a un mueble.</summary>
        [HttpPost]
        public async Task<IActionResult> Crear(MuebleConfiguracionRequestDto dto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }

            Mueble mueble = _mueble.GetById(dto.IdMueble);
            if (mueble is null)
            { return BadRequest("El mueble no existe."); }

            Configuracion configuracion = _configuracion.GetById(dto.IdConfiguracion);
            if (configuracion is null)
            { return BadRequest("La configuracion no existe."); }

            // Mano de obra y uso de maquinas ya se aplican siempre: no se agregan como recargo
            if (ClavesConfiguracion.EsProtegida(configuracion.Clave))
            { return BadRequest($"\"{configuracion.Clave}\" ya se aplica siempre a todos los muebles."); }

            if (mueble.ConfiguracionesExtra.Any(e => e.IdConfiguracion == dto.IdConfiguracion))
            { return BadRequest($"El mueble ya tiene el recargo \"{configuracion.Clave}\"."); }

            var muebleConfiguracion = _mapper.Map<MuebleConfiguracion>(dto);
            _muebleConfiguracion.Save(muebleConfiguracion);
            return Ok(muebleConfiguracion.Id);
        }

        /// <summary>DELETE api/MuebleConfiguraciones?Id=...: quita un recargo del mueble.</summary>
        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            MuebleConfiguracion muebleConfiguracionBack = _muebleConfiguracion.GetById(Id.Value);
            if (muebleConfiguracionBack is null)
            { return NotFound(); }
            _muebleConfiguracion.Delete(muebleConfiguracionBack.Id);
            return Ok();
        }
    }
}