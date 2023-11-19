
using ShortenerUrl.DAL.Interfaces;
using System.Text.RegularExpressions;

namespace ShortenerUrl.BLL.Providers
{
    public class UrlShorteningProvider
    {
        public const int NumberOfCharsInShortLink = 7;

        private const string Alphabet = "23456789abcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";

        private  readonly Random _random = new Random();

        private readonly IShortendUrlRepository _context;

        public UrlShorteningProvider(IShortendUrlRepository context)
        {
            this._context = context;
        }

        public async Task<string> GenerateUniqueShortUrlAsync()
        {
            while(true)
            {
                var urlChar = new char[NumberOfCharsInShortLink];
                for (int i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    var randomIndex = _random.Next(Alphabet.Length - 1);

                    urlChar[i] = Alphabet[randomIndex];
                }
                var url = new string(urlChar);

                if (await _context.GetShortendUrlByCodeAsync(url, false) == null)
                {
                    return url;
                } 
               
            }
           
        }

        public  bool ValidUrl(string url)
        {
            var checkUrl =  new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
            return checkUrl.IsMatch(url);
        }


    }
}
