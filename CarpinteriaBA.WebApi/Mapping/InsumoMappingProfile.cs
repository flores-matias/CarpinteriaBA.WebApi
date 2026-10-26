using AutoMapper;
using CarpinteriaBA.Application.DTOs.Insumo;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class InsumoMappingProfile : Profile
    {
        public InsumoMappingProfile()
        {
            CreateMap<Insumo, InsumoResponseDto>();

            // El precio de costo NO se copia con AutoMapper: solo se cambia con
            // Insumo.ActualizarPrecioCosto(), que valida que sea mayor a 0 (ultima correccion)
            CreateMap<InsumoRequestDto, Insumo>().ForMember(dest => dest.PrecioCostoActual, opt => opt.Ignore());
        }
    }
}