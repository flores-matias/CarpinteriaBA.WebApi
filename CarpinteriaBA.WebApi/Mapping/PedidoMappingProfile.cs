using AutoMapper;
using CarpinteriaBA.Application.DTOs.Pedido;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class PedidoMappingProfile : Profile
    {
        public PedidoMappingProfile()
        {
            // TotalPagado y Saldo se copian solos (se llaman igual en la entidad y en el DTO)
            CreateMap<Pedido, PedidoResponseDto>()
                .ForMember(dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.Cliente.Nombre + " " + src.Cliente.Apellido));

            // Solo copia IdCliente, FechaEntregaPrometida y EstadoPedido (lo que trae el DTO).
            // FechaCreacion, TotalAPagar y EstadoPago no se tocan.
            CreateMap<PedidoRequestDto, Pedido>();
        }
    }
}