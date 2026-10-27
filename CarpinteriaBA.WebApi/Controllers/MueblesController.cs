using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Mueble;
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
    public class MueblesController : ControllerBase
    {
        private readonly ILogger<MueblesController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Mueble> _mueble;
        private readonly IApplication<Configuracion> _configuracion;
        private readonly IMapper _mapper;

        public MueblesController(ILogger<MueblesController> logger,
            IStringService stringService,
            IApplication<Mueble> mueble,
            IApplication<Configuracion> configuracion,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _mueble = mueble;
            _configuracion = configuracion;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var margenes = ObtenerMargenes();
            var muebles = _mueble.GetAll();
            var resultado = _mapper.Map<IList<MuebleResponseDto>>(muebles);

            // Se completa el precio calculado de cada mueble con los margenes actuales
            foreach (var dto in resultado)
            {
                dto.PrecioCalculado = CalcularPrecio(dto.CostoMateriales, margenes);
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Mueble mueble = _mueble.GetById(Id.Value);
            if (mueble is null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<MuebleResponseDto>(mueble);
            dto.PrecioCalculado = CalcularPrecio(dto.CostoMateriales, ObtenerMargenes());
            return Ok(dto);
        }

        
        // GET api/Muebles/Costos?Id=...
        // Desglose: materiales + mano de obra + uso de maquinas = precio calculado.
        [HttpGet]
        [Route("Costos")]
        public async Task<IActionResult> Costos(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Mueble mueble = _mueble.GetById(Id.Value);
            if (mueble is null)
            {
                return NotFound();
            }

            var margenes = ObtenerMargenes();
            decimal materiales = mueble.CostoMateriales;
            decimal precioCalculado = CalcularPrecio(materiales, margenes);

            return Ok(new MuebleCostoDto
            {
                IdMueble = mueble.Id,
                Mueble = mueble.Nombre,
                CostoMateriales = materiales,
                ProporcionManoDeObra = margenes.ManoDeObra,
                ManoDeObra = Math.Round(materiales * margenes.ManoDeObra, 2),
                ProporcionUsoDeMaquinas = margenes.UsoDeMaquinas,
                UsoDeMaquinas = Math.Round(materiales * margenes.UsoDeMaquinas, 2),
                PrecioCalculado = precioCalculado,
                PrecioSugerido = mueble.PrecioSugerido,
                Diferencia = mueble.PrecioSugerido - precioCalculado,
                TieneReceta = mueble.RecetasMuebles.Any(),
                MargenesConfigurados = margenes.Configurados
            });
        }

        
        // PATCH api/Muebles/UsarPrecioCalculado?Id=...
        // Copia el precio calculado al precio sugerido (boton "Usar precio calculado").
        [HttpPatch]
        [Route("UsarPrecioCalculado")]
        public async Task<IActionResult> UsarPrecioCalculado(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            Mueble muebleBack = _mueble.GetById(Id.Value);
            if (muebleBack is null)
            { return NotFound(); }

            if (!muebleBack.RecetasMuebles.Any())
            { return BadRequest("El mueble no tiene receta cargada: no hay precio calculado para usar."); }

            var margenes = ObtenerMargenes();
            if (!margenes.Configurados)
            { return BadRequest($"Faltan los margenes en Configuracion (\"{ClavesConfiguracion.ManoDeObra}\" y \"{ClavesConfiguracion.UsoDeMaquinas}\")."); }

            muebleBack.PrecioSugerido = CalcularPrecio(muebleBack.CostoMateriales, margenes);
            _mueble.Save(muebleBack);

            return Ok(muebleBack.PrecioSugerido);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(MuebleRequestDto muebleRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var mueble = _mapper.Map<Mueble>(muebleRequestDto);
            _mueble.Save(mueble);
            return Ok(mueble.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, MuebleRequestDto muebleRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            Mueble muebleBack = _mueble.GetById(Id.Value);

            if (muebleBack is null)
            { return NotFound(); }

            _mapper.Map(muebleRequestDto, muebleBack);

            _mueble.Save(muebleBack);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            Mueble muebleBack = _mueble.GetById(Id.Value);
            if (muebleBack is null)
            { return NotFound(); }
            _mueble.Delete(muebleBack.Id);
            return Ok();
        }

        // ---- Calculo de precio ----

        //Margenes de Configuracion. Si falta alguno, cuenta como 0
        private (decimal ManoDeObra, decimal UsoDeMaquinas, bool Configurados) ObtenerMargenes()
        {
            var configuraciones = _configuracion.GetAll();
            var manoDeObra = configuraciones.FirstOrDefault(c => c.Clave == ClavesConfiguracion.ManoDeObra);
            var usoDeMaquinas = configuraciones.FirstOrDefault(c => c.Clave == ClavesConfiguracion.UsoDeMaquinas);

            return (manoDeObra?.Valor ?? 0,
                    usoDeMaquinas?.Valor ?? 0,
                    manoDeObra is not null && usoDeMaquinas is not null);
        }

        //Precio = materiales x (1 + mano de obra + uso de maquinas). Ej: 10.000 x 2,6 = 26.000
        private static decimal CalcularPrecio(decimal costoMateriales,
            (decimal ManoDeObra, decimal UsoDeMaquinas, bool Configurados) margenes)
        {
            return Math.Round(costoMateriales * (1 + margenes.ManoDeObra + margenes.UsoDeMaquinas), 2);
        }
    }
}