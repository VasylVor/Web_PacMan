using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Models;

namespace WebPacMan.Services
{
    public interface IManager
    {
        void Add(string key, GameField game);
        bool GetValue(string key, out GameField game);
    }
}
