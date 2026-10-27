using AutoMapper;
using CarpinteriaBA.Application.DTOs.Pago;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class PagoMappingProfile : Profile
    {
        public PagoMappingProfile()
        {
            CreateMap<Pago, PagoResponseDto>()
                .ForMember(dest => dest.TipoPago,
                    opt => opt.MapFrom(src => src.TipoPago.Descripcion))
                .ForMember(dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.Pedido.Cliente.Nombre + " " + src.Pedido.Cliente.Apellido));

            CreateMap<PagoRequestDto, Pago>();
        }
    }
}