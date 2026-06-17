using AutoMapper;
using CarpinteriaBA.Application.DTOs.Pedido;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class PedidoMappingProfile:Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<Pedido, PedidoResponseDto>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente.Nombre));
            CreateMap<PedidoRequestDto, Pedido>();
        }
    }
}
