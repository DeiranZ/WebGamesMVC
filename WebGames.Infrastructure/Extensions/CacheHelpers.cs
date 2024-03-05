using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGames.Infrastructure.Extensions
{
    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(10);
        private static readonly string gamesKeyTemplate = "game-{0}-{1}-{2}";

        public static string GenerateGamesPageCacheKey(int pageIndex, int gamesPerPage, int? genreId)
        {
            return string.Format(gamesKeyTemplate, pageIndex, gamesPerPage, genreId);
        }

        public static string GenerateGenresCacheKey()
        {
            return "genres";
        }

        public static string GenerateAllGamesCacheKey()
        {
            return "all-games";
        }
    }
}
