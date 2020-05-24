using AutoMapper;
using WebsiteManager.Mappers.Interfaces;
using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.Mappers
{
    public class WebsiteDataMapper : IWebsiteDataMapper
    {
        public WebsiteViewData Map (Website website)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Website, WebsiteViewData>()
                .ForMember(dest => dest.LoginDetails, 
                    opt => opt.MapFrom(
                        src => new LoginDetails() {
                            Email = src.Email,
                            Password = src.Password }));
            });

            var mapper = configuration.CreateMapper();

            return mapper.Map<WebsiteViewData>(website);
        }
    }
}
