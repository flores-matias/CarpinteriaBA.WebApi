using AutoMapper;
using CarpinteriaBA.Application.DTOs.Mueble;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class MuebleMappingProfile : Profile
    {
        public MuebleMappingProfile()
        {
            // CostoMateriales se copia solo (se llama igual en la entidad).
            // PrecioCalculado lo completa el controller, porque necesita los margenes de Configuracion.
            CreateMap<Mueble, MuebleResponseDto>()
                .ForMember(dest => dest.PrecioCalculado, opt => opt.Ignore());

            CreateMap<MuebleRequestDto, Mueble>();
        }
    }
}