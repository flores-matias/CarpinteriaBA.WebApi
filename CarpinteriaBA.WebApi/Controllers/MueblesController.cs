using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.Mueble;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MueblesController : ControllerBase
    {
        private readonly ILogger<MueblesController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<Mueble> _mueble;
        private readonly IMapper _mapper;

        public MueblesController(ILogger<MueblesController> logger,
            IStringService stringService,
            IApplication<Mueble> mueble,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _mueble = mueble;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<MuebleResponseDto>>(_mueble.GetAll()));
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
            return Ok(_mapper.Map<MuebleResponseDto>(mueble));
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
    }
}
