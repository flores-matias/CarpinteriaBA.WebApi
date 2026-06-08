using AutoMapper;
using CarpinteriaBA.Application.DTOs.Configuracion;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class ConfiguracionMappingProfile:Profile
    {
        public ConfiguracionMappingProfile()
        {
            CreateMap<Configuracion, ConfiguracionResponseDto>();
            CreateMap<ConfiguracionRequestDto, Configuracion>();
        }
    }
}
