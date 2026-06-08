using AutoMapper;
using CarpinteriaBA.Application.DTOs.DetallePedido;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class DetallePedidoMappingProfile:Profile
    {
        public DetallePedidoMappingProfile()
        {
            CreateMap<DetallePedido, DetallePedidoResponseDto>();
            CreateMap<DetallePedidoRequestDto, DetallePedido>();
        }
    }
}
