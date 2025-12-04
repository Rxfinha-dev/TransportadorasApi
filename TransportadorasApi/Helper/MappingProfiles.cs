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

            CreateMap<PedidoDto, Pedido>();
            CreateMap<Pedido, PedidoDto>().ForMember(dest => dest.ItensIds,
                   opt => opt.MapFrom(src => src.PedidoItems.Select(pi => pi.ItemId)));



        }
    }
    
}
