using AutoMapper;
using CarpinteriaBA.Application.DTOs.Proveedor;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class ProveedorMappingProfile:Profile
    {
        public ProveedorMappingProfile()
        {
            CreateMap<Proveedor,ProveedorResponseDto>();
            CreateMap<ProveedorRequestDto, Proveedor>();
        }
    }
}
