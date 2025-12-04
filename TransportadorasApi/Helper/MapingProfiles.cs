using AutoMapper;
using TransportadorasApi.Dto;
using TransportadorasApi.Model;

namespace TransportadorasApi.Helper
{
    public class MapingProfiles : Profile
    {
        public MapingProfiles()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
        }
    }
}
