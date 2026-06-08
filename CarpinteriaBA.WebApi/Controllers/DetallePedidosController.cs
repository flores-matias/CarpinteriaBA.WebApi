using AutoMapper;
using CarpinteriaBA.Application;
using CarpinteriaBA.Application.DTOs.DetallePedido;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CarpinteriaBA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidosController : ControllerBase
    {
        private readonly ILogger<DetallePedidosController> _logger;
        private readonly IStringService _stringService;
        private readonly IApplication<DetallePedido> _detallePedido;
        private readonly IMapper _mapper;

        public DetallePedidosController(ILogger<DetallePedidosController> logger,
            IStringService stringService,
            IApplication<DetallePedido> detallePedido,
            IMapper mapper)
        {
            _logger = logger;
            _stringService = stringService;
            _detallePedido = detallePedido;
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
            if (detallePedido == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DetallePedidoResponseDto>(detallePedido));
        }


        [HttpPost]
        public async Task<IActionResult> Crear(DetallePedidoRequestDto detallePedidoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }

            var detallePedido = _mapper.Map<DetallePedido>(detallePedidoRequestDto);
            _detallePedido.Save(detallePedido);
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
            { return BadRequest(); }

            _mapper.Map(detallePedidoRequestDto, detallePedidoBack);

            _detallePedido.Save(detallePedidoBack);

            return Ok(detallePedidoBack.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
            { return BadRequest(); }

            DetallePedido detallePedidoBack = _detallePedido.GetById(id.Value);

            if (detallePedidoBack is null)
            { return NotFound(); }

            _detallePedido.Delete(detallePedidoBack.Id);

            return Ok();
        }
    }
}
