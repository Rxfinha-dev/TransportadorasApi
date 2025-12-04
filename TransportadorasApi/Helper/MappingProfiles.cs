using AutoMapper;
using TransportadorasApi.Dto;
using TransportadorasApi.Model;

namespace TransportadorasApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();

            CreateMap<PedidoCreateDto, Pedido>()
                .ForMember(dest => dest.PedidoItems, opt => opt.Ignore());

            CreateMap<PedidoUpdateDto, Pedido>()
                .ForMember(dest => dest.PedidoItems, opt => opt.Ignore());
        }
    }
    
}
