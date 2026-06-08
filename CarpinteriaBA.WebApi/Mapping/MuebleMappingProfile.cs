using AutoMapper;
using CarpinteriaBA.Application.DTOs.Mueble;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class MuebleMappingProfile:Profile
    {
        public MuebleMappingProfile()
        {
            CreateMap<Mueble, MuebleResponseDto>();
            CreateMap<MuebleRequestDto, Mueble>();
        }
    }
}
