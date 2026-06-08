using AutoMapper;
using CarpinteriaBA.Application.DTOs.Pago;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class PagoMappingProfile:Profile
    {
        public PagoMappingProfile()
        {
            CreateMap<Pago, PagoResponseDto>();
            CreateMap<PagoRequestDto, Pago>();
        }
    }
}
