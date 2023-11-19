using ShortenerUrl.BLL.Models;
using ShortenerUrl.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortenerUrl.BLL.Interfaces
{
    public interface IServiceShortenerUrl
    {
        Task<List<ShortendUrlView>> IndexAsync();

        Task<string> CreateAsync(string longUrl);

        Task<ShortendUrl> GetShortendUrlAsync(int id, bool trackChanges);

        Task UpdateShortendUrlAsync(ShortendUrl shortend);

        Task DeleteAsync(int id);

        Task<ShortendUrl> GoToSite(string code);

    }
}
