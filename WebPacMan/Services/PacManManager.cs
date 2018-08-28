using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Models;

namespace WebPacMan.Services
{
    public class PacManManager : IManager
    {
        private IMemoryCache _cache;

        public PacManManager(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void Add(string key,GameField game)
        {
            _cache.Set(key, game);
        }

        public bool GetValue(string key, out GameField game)
        {
            if (_cache.TryGetValue(key, out game))
                return true;
            else
                return false;
        }
    }
}
