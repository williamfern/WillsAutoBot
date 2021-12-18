using AutoMapper;
using WillsAutoBot.Services.Models;
using WillsAutoBot.WebApi.Models;

namespace WillsAutoBot.WebApi.Config
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CoinApiModel, Coin>().ReverseMap();
        }
    }
}