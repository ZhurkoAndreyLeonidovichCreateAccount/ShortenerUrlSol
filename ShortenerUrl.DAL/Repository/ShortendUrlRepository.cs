using Microsoft.EntityFrameworkCore;
using ShortenerUrl.DAL.Data;
using ShortenerUrl.DAL.Entity;
using ShortenerUrl.DAL.Interfaces;

namespace ShortenerUrl.DAL.Repository
{
    public class ShortendUrlRepository: RepositoryBase<ShortendUrl>, IShortendUrlRepository
    {
      
        public ShortendUrlRepository(ApplicationDbContext context) :base(context) 
        {
           
        }

        public async Task<List<ShortendUrl>> GetAllShortendUrlsAsync(bool trackChanges)
        {
            var shortends = await FindAll(trackChanges).OrderBy(s=>s.DateOfCreation).ToListAsync();
            return shortends;
        }
        public async Task CreateShortendUrlAsync(ShortendUrl shortend) => await CreateAsync(shortend);
        public async Task DeleteShortendUrlAsync(ShortendUrl shortend) => await DeleteAsync(shortend);
        public async Task UpdateShortendUrlAsync(ShortendUrl shortend) => await UpdateAsync(shortend);



        public async Task<ShortendUrl?> GetShortendUrlAsync(int id, bool trackChanges)
        {
            return await FindByCondition(s => s.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<ShortendUrl?> GetShortendUrlByCodeAsync(string code, bool trackChanges)
        {
            return await FindByCondition(s => s.Code == code, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<ShortendUrl?> GetShortendUrlByLongUrlAsync(string longUrl, bool trackChanges)
        {
            var shortend = await FindByCondition(s => s.LongUrl == longUrl, trackChanges).SingleOrDefaultAsync();
            return shortend;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

       
    }
}

