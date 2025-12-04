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
            CreateMap<EnderecoDto, Endereco>();
            CreateMap<Endereco, EnderecoDto>();

            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.ItensIds,
                           opt => opt.MapFrom(src => src.PedidoItems.Select(pi => pi.ItemId)));

            CreateMap<PedidoDto, Pedido>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Rota, opt => opt.Ignore());








        }
    }
    
}
