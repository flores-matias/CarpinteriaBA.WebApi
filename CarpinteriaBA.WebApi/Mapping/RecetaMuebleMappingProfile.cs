using AutoMapper;
using CarpinteriaBA.Application.DTOs.RecetaMueble;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class RecetaMuebleMappingProfile : Profile
    {
        public RecetaMuebleMappingProfile()
        {
            // Subtotal se copia solo (se llama igual en la entidad y en el DTO)
            CreateMap<RecetaMueble, RecetaMuebleResponseDto>()
                .ForMember(dest => dest.Insumo, opt => opt.MapFrom(src => src.Insumo.Nombre))//aca lo que hace es que en el DTO, la propiedad Insumo es un string, mientras que en la entidad RecetaMueble es un objeto de tipo Insumo. Entonces, al mapear, se toma el nombre del insumo y se asigna a la propiedad Insumo del DTO.
                .ForMember(dest => dest.UnidadMedida, opt => opt.MapFrom(src => src.Insumo.UnidadMedida))
                .ForMember(dest => dest.PrecioCostoActual, opt => opt.MapFrom(src => src.Insumo.PrecioCostoActual));

            CreateMap<RecetaMuebleRequestDto, RecetaMueble>();
        }
    }
}