using AutoMapper;
using CarpinteriaBA.Application.DTOs.Pedido;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class PedidoMappingProfile:Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<Pedido, PedidoResponseDto>();
            CreateMap<PedidoRequestDto, Pedido>();
        }
    }
}
