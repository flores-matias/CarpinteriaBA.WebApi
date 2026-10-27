using AutoMapper;
using CarpinteriaBA.Application.DTOs.DetallePedido;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class DetallePedidoMappingProfile : Profile
    {
        public DetallePedidoMappingProfile()
        {
            // Subtotal se copia solo (se llama igual en la entidad y en el DTO)
            CreateMap<DetallePedido, DetallePedidoResponseDto>()
                .ForMember(dest => dest.Mueble, opt => opt.MapFrom(src => src.Mueble.Nombre));

            CreateMap<DetallePedidoRequestDto, DetallePedido>();
        }
    }
}