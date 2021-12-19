using AutoMapper;
using WillsAutoBot.Data.Entities;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.Core.Mappers
{
    public class CoinMapperProfile : Profile
    {
        public CoinMapperProfile()
        {
            CreateMap<CoinEntity, Coin>()
                .ReverseMap();

            CreateMap<CoinPriceEntity, CoinPrice>()
                .ReverseMap();
        }
    }
}