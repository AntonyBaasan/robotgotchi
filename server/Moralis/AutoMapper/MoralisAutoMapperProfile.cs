using AutoMapper;
using Moralis.Models;
using Robotgotchi.Dto.Nft;

namespace Moralis.AutoMapper
{
    public class MoralisAutoMapperProfile : Profile
    {
        public MoralisAutoMapperProfile()
        {
            CreateMap<MoralisNftObject, NftModel>();
            CreateMap<MoralisNftMetadata, NftMetadata>();
            CreateMap<MoralisNftProperty, NftProperty>();

            CreateMap<NftModel, MoralisNftObject>();
            CreateMap<NftMetadata, MoralisNftMetadata>();
            CreateMap<NftProperty, MoralisNftProperty>();
        }
    }
}
