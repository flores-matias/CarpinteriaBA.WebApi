using AutoMapper;
using CarpinteriaBA.Application.DTOs.Insumo;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class InsumoMappingProfile:Profile
    {
        public InsumoMappingProfile()
        {
            CreateMap<Insumo, InsumoResponseDto>();
            CreateMap<InsumoRequestDto, Insumo>();
        }
    }
}
