using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Application.DTOs.RecetaMueble;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarpinteriaBA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaMueblesController : ControllerBase
    {
        private readonly ILogger<RecetaMueblesController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<RecetaMueble> _recetaMueble;
        private readonly IMapper _mapper;

        public RecetaMueblesController(ILogger<RecetaMueblesController> logger,
            IStringService stringService,
            IApplication<RecetaMueble> recetaMueble,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _recetaMueble = recetaMueble;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<RecetaMuebleResponseDto>>(_recetaMueble.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            RecetaMueble recetaMueble = _recetaMueble.GetById(Id.Value);
            if (recetaMueble is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RecetaMuebleResponseDto>(recetaMueble));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(RecetaMuebleRequestDto recetaMuebleRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var recetaMueble = _mapper.Map<RecetaMueble>(recetaMuebleRequestDto);
            _recetaMueble.Save(recetaMueble);
            return Ok(recetaMueble.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, RecetaMuebleRequestDto recetaMuebleRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(); }

            RecetaMueble recetaMuebleBack = _recetaMueble.GetById(Id.Value);

            if (recetaMuebleBack is null)
            { return NotFound(); }

            _mapper.Map(recetaMuebleRequestDto, recetaMuebleBack);

            _recetaMueble.Save(recetaMuebleBack);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            RecetaMueble recetaMuebleBack = _recetaMueble.GetById(Id.Value);
            if (recetaMuebleBack is null)
            { return NotFound(); }
            _recetaMueble.Delete(recetaMuebleBack.Id);
            return Ok();
        }
    }
}
