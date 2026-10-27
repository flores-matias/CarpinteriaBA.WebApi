using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Configuracion;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Entities.MicrosoftIdentity;
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
    public class ConfiguracionesController : ControllerBase
    {
        private readonly ILogger<ConfiguracionesController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Configuracion> _configuracion;
        private readonly IMapper _mapper;

        public ConfiguracionesController(ILogger<ConfiguracionesController> logger,
            IStringService stringService,
            IApplication<Configuracion> configuracion,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _configuracion = configuracion;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]

        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<ConfiguracionResponseDto>>(_configuracion.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Configuracion configuracion = _configuracion.GetById(id.Value);
            if (configuracion == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ConfiguracionResponseDto>(configuracion));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ConfiguracionRequestDto configuracionRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // No puede haber dos configuraciones con la misma clave
            if (ExisteClave(configuracionRequestDto.Clave, idExcluido: null))
            {
                return BadRequest($"Ya existe una configuracion con la clave \"{configuracionRequestDto.Clave}\".");
            }

            var configuracion = _mapper.Map<Configuracion>(configuracionRequestDto);
            _configuracion.Save(configuracion);
            return Ok(configuracion.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? id, ConfiguracionRequestDto configuracionRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            Configuracion configuracionBack = _configuracion.GetById(id.Value);

            if (configuracionBack is null)
            { return NotFound(); }

            // Las claves que usa el calculo de precios no se pueden renombrar (el valor si se puede cambiar)
            if (ClavesConfiguracion.EsProtegida(configuracionBack.Clave)
                && configuracionRequestDto.Clave != configuracionBack.Clave)
            {
                return BadRequest($"La clave \"{configuracionBack.Clave}\" la usa el sistema para calcular precios: se puede cambiar su valor, pero no su nombre.");
            }

            if (ExisteClave(configuracionRequestDto.Clave, idExcluido: configuracionBack.Id))
            {
                return BadRequest($"Ya existe una configuracion con la clave \"{configuracionRequestDto.Clave}\".");
            }

            _mapper.Map(configuracionRequestDto, configuracionBack);

            _configuracion.Save(configuracionBack);

            return Ok(configuracionBack.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
            { return BadRequest(); }

            Configuracion configuracionBack = _configuracion.GetById(id.Value);

            if (configuracionBack is null)
            { return NotFound(); }

            if (ClavesConfiguracion.EsProtegida(configuracionBack.Clave))
            {
                return BadRequest($"La clave \"{configuracionBack.Clave}\" la usa el sistema para calcular precios y no se puede eliminar.");
            }

            _configuracion.Delete(configuracionBack.Id);
            return Ok();
        }

        //True si ya hay otra configuracion con esa clave (sin distinguir mayusculas).</summary>
        private bool ExisteClave(string clave, int? idExcluido)
        {
            return _configuracion.GetAll().Any(c =>
                c.Id != idExcluido
                && string.Equals(c.Clave.Trim(), clave.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}