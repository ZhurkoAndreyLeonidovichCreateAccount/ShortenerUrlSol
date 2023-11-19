using AutoMapper;
using ShortenerUrl.BLL.Interfaces;
using ShortenerUrl.BLL.Models;
using ShortenerUrl.BLL.Providers;
using ShortenerUrl.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using ShortenerUrl.DAL.Entity;

namespace ShortenerUrl.BLL.Services
{
    public class ServiceShortenerUrl : IServiceShortenerUrl
    {
        private readonly IShortendUrlRepository _context;
        private readonly UrlShorteningProvider _provider;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceShortenerUrl(IShortendUrlRepository context, UrlShorteningProvider provider, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _provider = provider;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ShortendUrlView>> IndexAsync()
        {
            
            var allShortendUrl = await _context.GetAllShortendUrlsAsync(false);
            var allShortendUrlView = _mapper.Map<List<ShortendUrlView>>(allShortendUrl);
            var schema = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;
            for (int i = 0; i < allShortendUrl.Count(); i++)
            {
                allShortendUrlView[i].ShortUrl = $"{schema}://{host}/{allShortendUrl[i].Code}";
            }
            return allShortendUrlView;
        }

        public async Task<string> CreateAsync(string longUrl)
        {
            string error = "";
            if (!_provider.ValidUrl(longUrl))
            {
                error = "This LongUrl is not valid";
                return error;
            }
            else if (await _context.GetShortendUrlByLongUrlAsync(longUrl, false) != null)
            {
                error = "This LongUrl already exist";
                return error;
            }


            var shortend = new ShortendUrl
            {
                LongUrl = longUrl,
                Code = await _provider.GenerateUniqueShortUrlAsync(),
                DateOfCreation = DateTime.UtcNow,
                NumberOfTransitions = 0,
            };
            await _context.CreateShortendUrlAsync(shortend);
            return error;


        }

        public async Task<ShortendUrl> GetShortendUrlAsync(int id, bool trackChanges)
        {
            
            var shortendUrl = await _context.GetShortendUrlAsync(id, trackChanges);
            return shortendUrl;
        }

        public async Task DeleteAsync(int id)
        {
            var shortendUrl = await _context.GetShortendUrlAsync(id, true);
            if (shortendUrl is not null)
            {
                await _context.DeleteShortendUrlAsync(shortendUrl);

            }
        }

        public async Task<ShortendUrl> GoToSite(string code)
        {
            var shortend = await _context.GetShortendUrlByCodeAsync(code, true);
            if (shortend is not null)
            {
                shortend.NumberOfTransitions += 1;
                await _context.SaveAsync();               
            }
            return shortend;
        }

        public async Task UpdateShortendUrlAsync(ShortendUrl shortend)
        {
           await _context.UpdateShortendUrlAsync(shortend);
        }
    }
}
