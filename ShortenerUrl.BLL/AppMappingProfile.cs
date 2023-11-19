using AutoMapper;
using ShortenerUrl.BLL.Models;
using ShortenerUrl.DAL.Entity;

namespace ShortenerUrl.BLL
{
    public class AppMappingProfile : Profile
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public AppMappingProfile()
        {

            CreateMap<ShortendUrl, ShortendUrlView>();

            //CreateMap<ShortendUrl, ShortendUrlView>()
            //.ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(
            //    src => $"{_httpContextAccessor.HttpContext.Request.Scheme}/{_httpContextAccessor.HttpContext.Request.Host}/{src.Code}"
            //    ));

        }

       
    }
}


