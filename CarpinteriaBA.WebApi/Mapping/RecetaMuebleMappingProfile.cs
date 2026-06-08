using AutoMapper;
using CarpinteriaBA.Application.DTOs.RecetaMueble;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class RecetaMuebleMappingProfile:Profile
    {
        public RecetaMuebleMappingProfile()
        {
            CreateMap<RecetaMueble, RecetaMuebleResponseDto>();
            CreateMap<RecetaMuebleRequestDto, RecetaMueble>();
        }
    }
}
