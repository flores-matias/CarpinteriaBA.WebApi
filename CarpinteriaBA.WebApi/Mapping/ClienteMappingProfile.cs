using AutoMapper;
using CarpinteriaBA.Application.DTOs.Cliente;
using CarpinteriaBA.Entities;

namespace CarpinteriaBA.WebApi.Mapping
{
    public class ClienteMappingProfile:Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Cliente, ClienteResponseDto>();
            CreateMap<ClienteRequestDto, Cliente>();

        }
    }
}
