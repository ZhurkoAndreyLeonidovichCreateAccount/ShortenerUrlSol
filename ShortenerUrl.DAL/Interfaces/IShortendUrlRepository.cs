
using ShortenerUrl.DAL.Entity;

namespace ShortenerUrl.DAL.Interfaces
{
    public interface IShortendUrlRepository
    {
        Task<List<ShortendUrl>> GetAllShortendUrlsAsync(bool trackChanges);

        Task<ShortendUrl?> GetShortendUrlAsync(int userId, bool trackChanges);

        Task<ShortendUrl?> GetShortendUrlByCodeAsync(string code, bool trackChanges);

        Task<ShortendUrl?> GetShortendUrlByLongUrlAsync(string shortUrl, bool trackChanges);

        Task CreateShortendUrlAsync(ShortendUrl shortend);

        Task DeleteShortendUrlAsync(ShortendUrl shortend);

        Task UpdateShortendUrlAsync(ShortendUrl shortend);

        Task SaveAsync();
    }
}
