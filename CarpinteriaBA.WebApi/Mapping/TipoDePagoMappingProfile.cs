using AutoMapper;
using CarpinteriaBA.Application.DTOs.TipoDePago;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class TipoDePagoMappingProfile:Profile
    {
        public TipoDePagoMappingProfile()
        {
            CreateMap<TipoPago, TipoPagoResponseDto>();
            CreateMap<TipoPagoRequestDto, TipoPago>();
        }
    }
}
