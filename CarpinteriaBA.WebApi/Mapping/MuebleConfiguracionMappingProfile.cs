using AutoMapper;
using CarpinteriaBA.Application.DTOs.MuebleConfiguracion;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class MuebleConfiguracionMappingProfile : Profile
    {
        public MuebleConfiguracionMappingProfile()
        {
            CreateMap<MuebleConfiguracion, MuebleConfiguracionResponseDto>()
                .ForMember(dest => dest.Clave, opt => opt.MapFrom(src => src.Configuracion.Clave))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Configuracion.Valor));

            CreateMap<MuebleConfiguracionRequestDto, MuebleConfiguracion>();
        }
    }
}