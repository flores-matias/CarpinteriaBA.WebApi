using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.Insumo;
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
    public class InsumosController : ControllerBase
    {
        private readonly ILogger<InsumosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Insumo> _insumo;
        private readonly IMapper _mapper;

        public InsumosController(ILogger<InsumosController> logger,
            IStringService stringService,
            IApplication<Insumo> insumo,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _insumo = insumo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<InsumoResponseDto>>(_insumo.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Insumo insumo = _insumo.GetById(Id.Value);
            if (insumo is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<InsumoResponseDto>(insumo));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(InsumoRequestDto insumoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var insumo = _mapper.Map<Insumo>(insumoRequestDto);
            //
            insumo.ActualizarPrecioCosto(insumoRequestDto.PrecioCostoActual);//Para que se registre el precio costo actual en la creación del insumo
            //
            _insumo.Save(insumo);
            return Ok(insumo.Id);
        }

        [HttpPut]
        //para cambiar el precio solo y no lo demas se usa el PATCH
        public async Task<IActionResult> Editar(int? Id, InsumoRequestDto insumoRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            Insumo insumoBack = _insumo.GetById(Id.Value);

            if (insumoBack is null)
            { return NotFound(); }

            _mapper.Map(insumoRequestDto, insumoBack);

            _insumo.Save(insumoBack);

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> EditarPrecioCosto(int? Id, decimal precioCostoActual)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            Insumo insumoBack = _insumo.GetById(Id.Value);
            if (insumoBack is null)
            { return NotFound(); }
            insumoBack.ActualizarPrecioCosto(precioCostoActual);
            _insumo.Save(insumoBack);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            Insumo insumoBack = _insumo.GetById(Id.Value);
            if (insumoBack is null)
            { return NotFound(); }
            _insumo.Delete(insumoBack.Id);
            return Ok();
        }
    }
}
